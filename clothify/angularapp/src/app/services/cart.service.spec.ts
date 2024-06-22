import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CartService } from './cart.service';

describe('CartService', () => {
  let service: CartService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CartService]
    });

    service = TestBed.inject(CartService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });
  
  fit('Frontend_should_get_all_products_from_cart_when_getAllProductsFromCart_is_called', () => {
    (service as any).getAllProductsFromCart().subscribe((products) => {
      expect(products).toBeTruthy();
    });
  
    const customerId = localStorage.getItem('customerId');
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/cart/customer/${customerId}`);
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBeTruthy();
  });

});