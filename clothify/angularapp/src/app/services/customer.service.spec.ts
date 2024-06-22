import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CustomerService } from './customer.service';

describe('CustomerService', () => {
  let service: CustomerService;
  let httpMock: HttpTestingController;


  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
        providers: [CustomerService] 
    });
    service = TestBed.inject(CustomerService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Frontend_should_register_a_customer_when_registerCustomer_is_called', () => {
    const customer = { 
      customerName: 'Customer Name', 
      address: 'cbe',
    };
    const response = { id: '1', ...customer };
  
    (service as any).registerCustomer(customer).subscribe();
    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/customer`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(customer);
    expect(req.request.headers.get('Authorization')).toBeTruthy();
    req.flush(response); 
  });
  
});
