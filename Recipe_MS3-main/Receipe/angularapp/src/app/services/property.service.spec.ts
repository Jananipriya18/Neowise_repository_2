// import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
// import { TestBed } from '@angular/core/testing';

// import { Property } from '../models/property.model'; // Changed from Recipe to Property
// import { PropertyService } from './property.service'; // Changed from RecipeService to PropertyService

// describe('PropertyService', () => { // Changed from RecipeService to PropertyService
//   let service: PropertyService; // Changed from RecipeService to PropertyService
//   let httpTestingController: HttpTestingController;

//   beforeEach(() => {
//     TestBed.configureTestingModule({
//       imports: [HttpClientTestingModule],
//       providers: [PropertyService], // Changed from RecipeService to PropertyService
//     });
//     service = TestBed.inject(PropertyService); // Changed from RecipeService to PropertyService
//     httpTestingController = TestBed.inject(HttpTestingController);
//   });

//   afterEach(() => {
//     httpTestingController.verify();
//   });

//   fit('PropertyService_should_be_created', () => { // Changed from RecipeService to PropertyService
//     expect(service).toBeTruthy();
//   });

//   fit('PropertyService_should_have_addProperty_method', () => { // Changed from RecipeService to PropertyService
//     expect(service.addProperty).toBeTruthy();
//   });

//   fit('PropertyService_should_have_getProperties_method', () => { // Changed from RecipeService to PropertyService
//     expect(service.getProperties).toBeTruthy();
//   });

//   fit('PropertyService_should_add_a_property_and_return_it', () => { // Changed from RecipeService to PropertyService
//     const mockProperty: Property = { // Changed from Recipe to Property
//       propertyId: 1, // Adjusted propertyId based on Property model
//       name: 'Test Property', // Adjusted name based on Property model
//       description: 'Test Description', // Adjusted description based on Property model
//       // Adjusted other properties based on Property model
//     };

//     service.addProperty(mockProperty).subscribe((property) => { // Changed from Recipe to Property
//       expect(property).toEqual(mockProperty);
//     });

//     const req = httpTestingController.expectOne(`${service['apiUrl']}api/Property`);
//     expect(req.request.method).toBe('POST');
//     req.flush(mockProperty);
//   });

//   fit('PropertyService_should_get_properties', () => { // Changed from RecipeService to PropertyService
//     const mockProperties: Property[] = [ // Changed from Recipe to Property
//       {
//         propertyId: 1, // Adjusted propertyId based on Property model
//         name: 'Test Property 1', // Adjusted name based on Property model
//         description: 'Test Description', // Adjusted description based on Property model
//         // Adjusted other properties based on Property model
//       }
//     ];

//     service.getProperties().subscribe((properties) => { // Changed from Recipe to Property
//       expect(properties).toEqual(mockProperties);
//     });

//     const req = httpTestingController.expectOne(`${service['apiUrl']}api/Property`);
//     expect(req.request.method).toBe('GET');
//     req.flush(mockProperties);
//   });
// });
