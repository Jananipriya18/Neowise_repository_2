import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { Property } from '../models/property.model';
import { PropertyService } from './property.service';

describe('PropertyService', () => {
  let service: PropertyService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PropertyService],
    });
    service = TestBed.inject(PropertyService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('PropertyService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('PropertyService_should_have_addProperty_method', () => {
    expect(service.addProperty).toBeTruthy();
  });

  fit('PropertyService_should_have_getProperties_method', () => {
    expect(service.getProperties).toBeTruthy();
  });

  fit('PropertyService_should_add_a_property_and_return_it', () => {
    const mockProperty: Property = {
      propertyId: 1,
      name: 'Test Property',
      description: 'Test Description',
      address: 'Test Address', // Adjusted to match the new interface
      propertyType: 'Test Property Type', // Adjusted to match the new interface
      bedrooms: 3, // Adjusted to match the new interface
      bathrooms: 2, // Adjusted to match the new interface
      monthlyRent: 2000, // Adjusted to match the new interface
      createdAt: new Date() // Adjusted to match the new interface
    };

    service.addProperty(mockProperty).subscribe((property) => {
      expect(property).toEqual(mockProperty);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Property`);
    expect(req.request.method).toBe('POST');
    req.flush(mockProperty);
});


  fit('PropertyService_should_get_properties', () => {
    const mockProperties: Property[] = [
      {
        propertyId: 1,
        name: 'Test property 1',
        description: 'Test Description',
        address: 'Test Address', // Adjusted to match the new interface
        propertyType: 'Test Property Type', // Adjusted to match the new interface
        bedrooms: 3, // Adjusted to match the new interface
        bathrooms: 2, // Adjusted to match the new interface
        monthlyRent: 2000, // Adjusted to match the new interface
        createdAt: new Date() // Adjusted to match the new interface
      }
    ];

    service.getProperties().subscribe((properties) => {
      expect(properties).toEqual(mockProperties);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Property`);
    expect(req.request.method).toBe('GET');
    req.flush(mockProperties);
  });
});