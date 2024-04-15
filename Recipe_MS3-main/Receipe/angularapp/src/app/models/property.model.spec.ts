// import { Property } from './property.model'; // Import the Property model

// describe('Property', () => {
//   fit('PropertyModel_should_create_an_instance', () => {
//     // Create a factory function to create instances of Property
//     function createProperty(
//       propertyId: number,
//       name: string,
//       description: string,
//       address: string,
//       propertyType: string,
//       bedrooms: number,
//       bathrooms: number,
//       monthlyRent: number,
//       available: boolean,
//       createdAt: Date
//     ): Property {
//       return {
//         propertyId,
//         name,
//         description,
//         address,
//         propertyType,
//         bedrooms,
//         bathrooms,
//         monthlyRent,
//         available,
//         createdAt
//       };
//     }

//     // Use the factory function to create an instance of Property
//     const property: Property = createProperty(
//       1,
//       'Test Property',
//       'Test Description',
//       'Test Address',
//       'Test Type',
//       3,
//       2,
//       1500,
//       true,
//       new Date()
//     );

//     // Assert that the instance is created successfully
//     expect(property).toBeDefined();
//   });

//   fit('PropertyModel_should_update_property_values_using_setters', () => {
//     // Similar setup as above
//     function createProperty(
//       propertyId: number,
//       name: string,
//       description: string,
//       address: string,
//       propertyType: string,
//       bedrooms: number,
//       bathrooms: number,
//       monthlyRent: number,
//       available: boolean,
//       createdAt: Date
//     ): Property {
//       return {
//         propertyId,
//         name,
//         description,
//         address,
//         propertyType,
//         bedrooms,
//         bathrooms,
//         monthlyRent,
//         available,
//         createdAt
//       };
//     }

//     const property: Property = createProperty(
//       1,
//       'Test Property',
//       'Test Description',
//       'Test Address',
//       'Test Type',
//       3,
//       2,
//       1500,
//       true,
//       new Date()
//     );

//     // Assert that the instance is created successfully
//     expect(property).toBeTruthy();
//     // Assertions for property values and data types remain the same
//     expect(property.propertyId).toBeTruthy();
//     expect(property.name).toBeTruthy();
//     expect(property.description).toBeTruthy();
//     expect(property.address).toBeTruthy();
//     expect(property.propertyType).toBeTruthy();
//     expect(property.bedrooms).toBeTruthy();
//     expect(property.bathrooms).toBeTruthy();
//     expect(property.monthlyRent).toBeTruthy();
//     expect(property.available).toBeTruthy();
//     expect(property.createdAt).toBeTruthy();
//     expect(typeof property.propertyId).toEqual('number');
//     expect(typeof property.name).toEqual('string');
//     expect(typeof property.description).toEqual('string');
//     expect(typeof property.address).toEqual('string');
//     expect(typeof property.propertyType).toEqual('string');
//     expect(typeof property.bedrooms).toEqual('number');
//     expect(typeof property.bathrooms).toEqual('number');
//     expect(typeof property.monthlyRent).toEqual('number');
//     expect(typeof property.available).toEqual('boolean');
//     expect(property.createdAt instanceof Date).toBeTruthy();
//   });
// });
