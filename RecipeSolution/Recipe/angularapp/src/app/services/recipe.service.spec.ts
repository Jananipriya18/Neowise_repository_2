import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { Laptop } from '../models/laptop.model';
import { LaptopService } from './laptop.service';

describe('LaptopService', () => {
  let service: LaptopService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [LaptopService],
    });
    service = TestBed.inject(LaptopService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('LaptopService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('LaptopService_should_add_a_laptop_and_return_it', () => {
    const mockLaptop: Laptop = {
      laptopId: 100,
      brand: 'Test Brand',
      model: 'Test Model',
      description: 'Test Description',
      processor: 'Test Processor',
      storage: 'Test Storage',
      price: 1000
    };

    service.addLaptop(mockLaptop).subscribe((laptop) => {
      expect(laptop).toEqual(mockLaptop);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Laptop`);
    expect(req.request.method).toBe('POST');
    req.flush(mockLaptop);
  });

  fit('LaptopService_should_get_laptops', () => {
    const mockLaptops: Laptop[] = [
      {
        laptopId: 100,
        brand: 'Test Brand 1',
        model: 'Test Model 1',
        description: 'Test Description',
        processor: 'Test Processor',
        storage: 'Test Storage',
        price: 1000
      }
    ];

    service.getLaptops().subscribe((laptops) => {
      expect(laptops).toEqual(mockLaptops);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Laptop`);
    expect(req.request.method).toBe('GET');
    req.flush(mockLaptops);
  });

  fit('LaptopService_should_delete_laptop', () => {
    const laptopId = 100;

    service.deleteLaptop(laptopId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Laptop/${laptopId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('LaptopService_should_get_laptop_by_id', () => {
    const laptopId = 100;
    const mockLaptop: Laptop = {
      laptopId: laptopId,
      brand: 'Test Brand',
      model: 'Test Model',
      description: 'Test Description',
      processor: 'Test Processor',
      storage: 'Test Storage',
      price: 1000
    };

    service.getLaptop(laptopId).subscribe((laptop) => {
      expect(laptop).toEqual(mockLaptop);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Laptop/${laptopId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockLaptop);
  });
});
