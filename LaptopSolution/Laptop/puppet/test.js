const puppeteer = require('puppeteer');

(async () => {
  const browser = await puppeteer.launch({
    headless: false,
    args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox'],
  });

  const page1 = await browser.newPage();
// Test Case: Check if table body is present
try {
  await page1.goto('https://api.example.com/viewLaptops');

  await page1.waitForSelector('table tbody tr', { timeout: 5000 });
  
      // Check if there are at least some rows in the table
  
      const rowCount = await page1.$$eval('table tbody tr', rows => rows.length, { timeout: 5000 });
  
      // console.log(rowCount);
      if (rowCount > 0) 
      {
        console.log('TESTCASE:Laptops_table_rows_exist:success');
      } 
      else 
      {
        console.log('TESTCASE:Laptops_table_rows_exist:failure');
      }
    } 
    catch (e) 
    {
        console.log('TESTCASE:Laptops_table_rows_exist:failure');
    }

  

  const page2 = await browser.newPage();
try {
  await page2.goto('https://api.example.com/addNewLaptop');

  // Test Case: Check if form exists and specific input fields are present
  const formExists = await page2.evaluate(() => {
    const form = document.querySelector('form');
    const inputFields = ['brand', 'model', 'description', 'processor', 'storage', 'price'];
    const formHasInputFields = inputFields.every(field => !!form.querySelector(`[name="${field}"]`));
    return !!form && formHasInputFields;
  });

  if (formExists) {
    console.log('TESTCASE:CreateLaptop_form_exists_and_input_fields_present_in_Create_Laptop:success');
  } else {
    console.log('TESTCASE:CreateLaptop_form_exists_and_input_fields_present_in_Create_Laptop:failure');
  }

} catch (e) {
  console.log('TESTCASE:CreateLaptop_form_exists_and_input_fields_present_in_Create_Laptop:failure');
}

const page3 = await browser.newPage();
  // Test Case: Check if table headers are present

try {
  await page3.goto('https://api.example.com/viewLaptops');

  // Wait for the table to be rendered
  // await page3.waitForSelector('table');
  await page3.waitForSelector('table', { timeout: 3000 });

  const tableHeaderContent = await page3.evaluate(() => {
    const thElements = Array.from(document.querySelectorAll('table th'));
    return thElements.map(th => th.textContent.trim());
  });

  const expectedHeaders = ['Brand', 'Model', 'Description', 'Processor', 'Storage', 'Price'];

  const headerMatch = expectedHeaders.every(header => tableHeaderContent.includes(header));

  if (headerMatch) {
    console.log('TESTCASE:Laptop_table_header_content:success');
  } else {
    console.log('TESTCASE:Laptop_table_header_content:failure');
  }
} catch (e) {
  console.log('TESTCASE:Laptop_table_header_content:failure');
}

const page4 = await browser.newPage();
try {
  await page4.goto('https://api.example.com/viewLaptops');
  await page4.setViewport({
    width: 1200,
    height: 1200,
  });
  await page4.waitForSelector('#delete', { timeout: 3000 }); // Wait for delete button

  await page4.click('#delete');

  await page4.waitForSelector('h2', { timeout: 2000 }); // Wait for confirmation message

  const confirmationMessage = await page4.$eval('h2', element => element.textContent.toLowerCase());

  const urlAfterClick = page4.url();

  if (confirmationMessage.includes("delete confirmation") && urlAfterClick.toLowerCase().includes('confirmdelete')) {
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:success');
  } else {
    console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure');
  }
} catch (error) {
  console.log('TESTCASE:Verify_Navigation_to_ConfirmDelete_Page_and_Details:failure', error);
}

const page5 = await browser.newPage();
try {
  await page5.goto('https://api.example.com/addNewLaptop');
  await page5.setViewport({
    width: 1200,
    height: 1200,
  });
  
  // Wait for the form elements to load
  await page5.waitForSelector('#brand');
  await page5.waitForSelector('#model');
  await page5.waitForSelector('#description');
  await page5.waitForSelector('#processor');
  await page5.waitForSelector('#storage');
  await page5.waitForSelector('#price');
  await page5.waitForSelector('button[type="submit"]');

  // Click the Add Laptop button without entering any data
  await page5.click('button[type="submit"]');

  // Wait for a short period for validation to take effect
  await page5.waitForTimeout(1000);
    
  // Define an array of field brands and their corresponding error messages
  const fieldsToCheck = [
    { id: '#brand', message: 'Brand is required' },
    { id: '#model', message: 'Model is required' },
    { id: '#description', message: 'Description is required' },
    { id: '#processor', message: 'Processor is required' },
    { id: '#storage', message: 'Storage is required' },
    { id: '#price', message: 'Price is required' }
  ];

  let isValidationFailed = false;

  // Iterate through each field and check its corresponding error message
  for (const fieldData of fieldsToCheck) {
    const errorMessage = await page5.$eval(fieldData.id + ' + .error-message', el => el.textContent);
    if (!errorMessage.includes(fieldData.message)) {
      isValidationFailed = true;
      // console.log(`Validation message for ${fieldData.message} field: failure - Expected message: ${fieldData.message}`);
    }
  }

  // Log a single failure message if any validation fails
  if (isValidationFailed) {
    console.log('TESTCASE:Verify_required_validation_on_Add_Laptop_button:failure');
  } else {
    console.log('TESTCASE:Verify_required_validation_on_Add_Laptop_button:success');
  }
} catch (error) {
  console.log('TESTCASE:Verify_required_validation_on_Add_Laptop_button:failure');
}



  finally{
    await page1.close();
    await page2.close();
    await page3.close();
    await page4.close();
    await page5.close();
  }
 

  await browser.close();
})();

