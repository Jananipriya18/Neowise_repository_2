// import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
// import { TestBed } from '@angular/core/testing';

// import { Recipe } from '../models/recipe.model';
// import { RecipeService } from './recipe.service';

// describe('RecipeService', () => {
//   let service: RecipeService;
//   let httpTestingController: HttpTestingController;

//   beforeEach(() => {
//     TestBed.configureTestingModule({
//       imports: [HttpClientTestingModule],
//       providers: [RecipeService],
//     });
//     service = TestBed.inject(RecipeService);
//     httpTestingController = TestBed.inject(HttpTestingController);
//   });

//   afterEach(() => {
//     httpTestingController.verify();
//   });

//   fit('RecipeService_should_be_created', () => {
//     expect(service).toBeTruthy();
//   });

//   // fit('RecipeService_should_have_addRecipe_method', () => {
//   //   expect(service.addRecipe).toBeTruthy();
//   // });

//   // fit('RecipeService_should_have_getRecipes_method', () => {
//   //   expect(service.getRecipes).toBeTruthy();
//   // });

//   fit('RecipeService_should_add_a_recipe_and_return_it', () => {
//     const mockRecipe: Recipe = {
//       tutorId: 1,
//       name: 'Test Recipe',
//       email: 'Test Email',
//       subjectsOffered: 'Test SubjectsOffered',
//       contactNumber: 'Test ContactNumber',
//       availability: 'Test Availability'
//     };

//     service.addRecipe(mockRecipe).subscribe((recipe) => {
//       expect(recipe).toEqual(mockRecipe);
//     });

//     const req = httpTestingController.expectOne(`${service['apiUrl']}api/Recipe`);
//     expect(req.request.method).toBe('POST');
//     req.flush(mockRecipe);
//   });

//   fit('RecipeService_should_get_recipes', () => {
//     const mockRecipes: Recipe[] = [
//       {
//         tutorId: 1,
//         name: 'Test Recipe 1',
//         email: 'Test Email',
//         subjectsOffered: 'Test SubjectsOffered',
//         contactNumber: 'Test ContactNumber',
//         availability: 'Test Availability'
//       }
//     ];

//     service.getRecipes().subscribe((recipes) => {
//       expect(recipes).toEqual(mockRecipes);
//     });

//     const req = httpTestingController.expectOne(`${service['apiUrl']}api/Recipe`);
//     expect(req.request.method).toBe('GET');
//     req.flush(mockRecipes);
//   });
// });



import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { Recipe } from '../models/recipe.model';
import { RecipeService } from './recipe.service';

describe('RecipeService', () => {
  let service: RecipeService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RecipeService],
    });
    service = TestBed.inject(RecipeService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  fit('RecipeService_should_be_created', () => {
    expect(service).toBeTruthy();
  });

  fit('RecipeService_should_add_a_recipe_and_return_it', () => {
    const mockRecipe: Recipe = {
      tutorId: 100,
      name: 'Test Recipe',
      email: 'Test Email',
      subjectsOffered: 'Test SubjectsOffered',
      contactNumber: 'Test ContactNumber',
      availability: 'Test Availability'
    };

    service.addRecipe(mockRecipe).subscribe((recipe) => {
      expect(recipe).toEqual(mockRecipe);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Recipe`);
    expect(req.request.method).toBe('POST');
    req.flush(mockRecipe);
  });

  fit('RecipeService_should_get_recipes', () => {
    const mockRecipes: Recipe[] = [
      {
        tutorId: 100,
        name: 'Test Recipe 1',
        email: 'Test Email',
        subjectsOffered: 'Test SubjectsOffered',
        contactNumber: 'Test ContactNumber',
        availability: 'Test Availability'
      }
    ];

    service.getRecipes().subscribe((recipes) => {
      expect(recipes).toEqual(mockRecipes);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Recipe`);
    expect(req.request.method).toBe('GET');
    req.flush(mockRecipes);
  });

  fit('RecipeService_should_delete_recipe', () => {
    const tutorId = 100;

    service.deleteRecipe(tutorId).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Recipe/${tutorId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });

  fit('RecipeService_should_get_recipe_by_id', () => {
    const tutorId = 100;
    const mockRecipe: Recipe = {
      tutorId: tutorId,
      name: 'Test Recipe',
      email: 'Test Email',
      subjectsOffered: 'Test SubjectsOffered',
      contactNumber: 'Test ContactNumber',
      availability: 'Test Availability'
    };

    service.getRecipe(tutorId).subscribe((recipe) => {
      expect(recipe).toEqual(mockRecipe);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}api/Recipe/${tutorId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockRecipe);
  });
});
