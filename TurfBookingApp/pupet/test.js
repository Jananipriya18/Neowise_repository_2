const { Console } = require('console');
const { url } = require('inspector');
const puppeteer = require('puppeteer');
(async () => {
    const browser = await puppeteer.launch({
      headless: false,
      args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox']
    });

    // Test case to verify the existence of correct heading, table, and back button in the booked batches page
    const page = await browser.newPage();
    try {
      await page.goto('https://8081-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/');
      await page.setViewport({
        width: 1200,
        height: 1200,
      });
  
      await page.waitForSelector('h1', { timeout: 2000 });
      await page.waitForSelector('table', { timeout: 2000 });

      const headers = await page.evaluate(() => {
        const thElements = Array.from(document.querySelectorAll('table th'));
        return thElements.map(th => th.textContent.trim());
      });
    //   console.log(headers);
      if (headers[0] === 'Turf ID' && headers[3] === 'Availability' && headers[4] === 'Action'){
        const rowCount = await page.$$eval('table tbody tr', rows => rows.length);
    // console.log(rowCount);
        if (rowCount > 0) {      
          await page.waitForSelector('table tbody tr', { timeout: 5000 } );
          console.log('TESTCASE:In_Index_Page_Table_Headers_and_Rows_are_Correct_and_Exists:success');
        }else{          
          console.log('TESTCASE:In_Index_Page_Table_Headers_and_Rows_are_Correct_and_Exists:failure');
        }
      }else{
        console.log('TESTCASE:In_Index_Page_Table_Headers_and_Rows_are_Correct_and_Exists:failure');
      }

    //   const rowCount = await page.$$eval('tr', rows => rows.length, { timeout: 2000 });
    //   if (rowCount>1) 
    //   {
    //     console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_in_index_page:success');
    //   } 
    //   else 
    //   {
    //   console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_index_page:failure');
    //   }
    } catch (e) {

      console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_index_page:failure');
    } 

    // Test case to verify the existence of book and delete buttons in the available batches page
    const page1 = await browser.newPage();
    try {
      await page1.goto('https://8081-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/');
      await page1.setViewport({
        width: 1200,
        height: 1200,
      });
      await page1.waitForSelector('#deleteButton', { timeout: 2000 });
      await page1.waitForSelector('#bookButton', { timeout: 2000 });
      const rowCount = await page1.$$eval('tr', rows => rows.length, { timeout: 2000 });
    
      if (rowCount >1) 
      {
        console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_batches_page:success');
      } 
      else 
      {
        console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_batches_page:failure');
      }
    } catch (e) {
      console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_batches_page:failure');
    }
    
    // Test case to verify the existence of back button and heading in the batch enrollment form page
    const page2 = await browser.newPage();
    try {
      await page2.goto('https://8081-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/');
      await page2.setViewport({
        width: 1200,
        height: 1200,
      });
      await page2.waitForSelector('#bookButton', { timeout: 2000 });
      await page2.click('#bookButton');
      const urlAfterClick = page2.url();
      await page2.waitForSelector('#backtoturf', { timeout: 2000 });
      const Message = await page2.$eval('h1', element => element.textContent.toLowerCase());
    if(Message.includes("make a reservation")&&urlAfterClick.toLowerCase().includes('/booking/book'))
    {
    console.log('TESTCASE:Existence_of_id_backtobatch_and_heading_in_batch_enrollment_form_page:success');
    }    
    else{
    console.log('TESTCASE:Existence_of_id_backtobatch_and_heading_in_batch_enrollment_form_page:failure');
    }
    } catch (e) {
      console.log('TESTCASE:Existence_of_id_backtobatch_and_heading_in_batch_enrollment_form_page:failure',e);
    } 

  finally{
    await page.close();
    await page1.close();
    await page2.close();
    await browser.close();
  }
  
})();