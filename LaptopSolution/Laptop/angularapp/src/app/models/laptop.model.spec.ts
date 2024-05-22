// import { Recipe } from '../models/recipe.model';

// describe('Recipe', () => {
//   fit('should_create_recipe_instance', () => {
//     const recipe: Recipe = {
//       recipeId: 1,
//       name: 'Test Recipe',
//       description: 'Test Description',
//       ingredients: 'Test Ingredients',
//       instructions: 'Test Instructions',
//       author: 'Test Author'
//     };

//     // Check if the recipe object exists
//     expect(recipe).toBeTruthy();

//     // Check individual properties of the recipe
//     expect(recipe.recipeId).toBe(1);
//     expect(recipe.name).toBe('Test Recipe');
//     expect(recipe.description).toBe('Test Description');
//     expect(recipe.ingredients).toBe('Test Ingredients');
//     expect(recipe.instructions).toBe('Test Instructions');
//     expect(recipe.author).toBe('Test Author');
// });

// });
import { Laptop } from './laptop.model';

describe('Laptop', () => {
  fit('should_create_laptop_instance', () => {
    const laptop: Laptop = {
      laptopId: 1,
      brand: 'Test brand',
      model: 'Test model',
      description: 'Test description',
      processor: 'Test processor',
      storage: 'Test storage',
      price: 1899
    };

    // Check if the laptop object exists
    expect(laptop).toBeTruthy();

    // Check individual properties of the laptop
    expect(laptop.laptopId).toBe(1);
    expect(laptop.brand).toBe('Test brand');
    expect(laptop.model).toBe('Test model');
    expect(laptop.description).toBe('Test description');
    expect(laptop.processor).toBe('Test processor');
    expect(laptop.storage).toBe('Test storage');
    expect(laptop.price).toBe(1899);
  });
});
