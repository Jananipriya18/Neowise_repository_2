import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';
 
interface Product {
  productType: string;
  productImageUrl: string;
  productDetails: string;
  productPrice: number;
  quantity: number;
}
 
@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  addProductForm: FormGroup;
  errorMessage = '';
  selectedFile: File | null = null;
  photoImage="";
 
  constructor(private fb: FormBuilder, private productService: ProductService, private route: Router) {
    this.addProductForm = this.fb.group({
      productType: ['', Validators.required],
      productImageUrl: [null, Validators.required],
      productDetails: ['', Validators.required],
      productPrice: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      quantity: ['', [Validators.required, Validators.min(1)]],
    });
  }
 
  ngOnInit(): void {
  }
 
  onSubmit(): void {
    if (this.addProductForm.valid) {
      const newProduct = this.addProductForm.value;
      const requestObj: Product = {
        productType: newProduct.productType,
        productImageUrl: this.photoImage,
        productDetails: newProduct.productDetails,
        productPrice: newProduct.productPrice,
        quantity: newProduct.quantity,
      };

      this.productService.addProduct(requestObj).subscribe(
        (response) => {
          console.log('Product added successfully', response);
          this.route.navigate(['/admin/products/view']);
          this.addProductForm.reset(); // Reset the form
        },
        (error) => {
          console.error('ErrorAddingProduct', error);
        }
      );
    } else {
      this.errorMessage = "All fields are required";
    }
  }
  onFileSelected(event: any) {
    const files: FileList = event.target.files;
    if (files.length > 0) {
      this.selectedFile = files[0];
    }
  }

  handleFileChange(event: any): void {
    const file = event.target.files[0];
 
    if (file) {
      this.convertFileToBase64(file).then(
        (base64String) => {
          this.photoImage=base64String
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
}