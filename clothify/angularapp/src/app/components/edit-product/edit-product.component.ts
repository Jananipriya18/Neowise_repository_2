import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';
import { Product } from 'src/app/models/product.model';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  editProductForm: FormGroup;
  product: Product;
  errorMessage = '';

  editedProduct: any;
  productId: any;
  photoImage="";
  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.editProductForm = this.fb.group({
      productType: ['', Validators.required],
      productImageUrl: ['', Validators.required],
      productDetails: ['', Validators.required],
      productPrice: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      quantity: ['', [Validators.required, Validators.min(1)]],
    });
  }
  ngOnInit(): void {
    // Retrieve the serialized product object from the query parameters
    const product = JSON.parse(this.route.snapshot.queryParamMap.get('product'));
  
    if (product) {
      // Populate the form with the existing product details
      this.editProductForm.patchValue({
        productType: product.productType,
        productImageUrl: this.photoImage,
        productDetails: product.productDetails,
        quantity: product.quantity, // Uncomment if 'quantity' is part of your product object
        productPrice: product.productPrice,
      });
      console.log(this.photoImage);
    } else {
      // Handle the case when the product object is not available
    }
  }
  onSubmit(): void {
    if (this.editProductForm.valid) {
      const updatedProduct = this.editProductForm.value;
   
      // Include the 'quantity' field in the request object
      const requestObj: Product = {
        productType: updatedProduct.productType,
        productImageUrl: this.photoImage,
        productDetails: updatedProduct.productDetails,
        productPrice: updatedProduct.productPrice,
        quantity: updatedProduct.quantity, // Add this line
      };

      // Fetch productId from the route params
      const productId = this.route.snapshot.paramMap.get('productId');
      console.log(requestObj, "photo");
      this.productService.updateProduct(productId, requestObj).subscribe(
        (response) => {
          console.log('Product updated successfully', response);
          this.product = response;

          this.router.navigate(['/admin/products/view']);
        },
        (error) => {
          console.error('Error updating product', error);
        }
      );
    } else {
      // this.errorMessage = 'All fields are required';
    }
  }
  
  handleFileChange(event: any): void {
    const file = event.target.files[0];
 
    if (file) {
      this.convertFileToBase64(file).then(
        (base64String) => {
          this.photoImage=base64String;
          console.log(this.photoImage, "final");
          
        },
        
        (error) => {
          console.error('Error converting file to base64:', error);
          // Handle error appropriately
        }
      );
    }
  }
 
  convertFileToBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
 
      reader.onload = () => {
        resolve(reader.result as string);
      };
 
      reader.onerror = (error) => {
        reject(error);
      };
 
      reader.readAsDataURL(file);
    });
  }

  onCancel(){
    this.router.navigate(['/admin/products/view'])
  }

}
