import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductService } from './product.service';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    });

    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Frontend_should_add_a_product_when_addProduct_is_called', () => {
    const product = { 
      productId : 1,
      productType: 'Product Name', 
      productDetails: 'Product Description',
      productImageUrl: 'Product Image URL',
      quantity: 10,
      productPrice: 1000
    };
    const response = { id: '1', ...product };
  
    (service as any).addProduct(product).subscribe();
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/product`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(product);
    expect(req.request.headers.get('Authorization')).toBeTruthy();
    req.flush(response); 
  });
  
  fit('Frontend_should_get_all_products_when_viewAllProducts_is_called', () => {
    (service as any).viewAllProducts().subscribe((products) => {
      expect(products).toBeTruthy();
    });
  
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/product`);
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBeTruthy();
  });

  
  fit('Frontend_should_delete_a_product_when_deleteProduct_is_called', () => {
    const productId = 1;
  
    (service as any).deleteProduct(productId).subscribe();
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/product/${productId}`);
    expect(req.request.method).toBe('DELETE');
    expect(req.request.headers.get('Authorization')).toBeTruthy();
  });

});