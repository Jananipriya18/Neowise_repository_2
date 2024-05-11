import { Recipe } from '../models/recipe.model';

describe('Recipe', () => {
  fit('should_create_recipe_instance', () => {
    const recipe: Recipe = {
      tutorId: 1,
      name: 'Test Recipe',
      email: 'Test Email',
      subjectsOffered: 'Test SubjectsOffered',
      contactNumber: 'Test ContactNumber',
      author: 'Test Author'
    };

    // Check if the recipe object exists
    expect(recipe).toBeTruthy();

    // Check individual properties of the recipe
    expect(recipe.tutorId).toBe(1);
    expect(recipe.name).toBe('Test Recipe');
    expect(recipe.email).toBe('Test Email');
    expect(recipe.subjectsOffered).toBe('Test SubjectsOffered');
    expect(recipe.contactNumber).toBe('Test ContactNumber');
    expect(recipe.author).toBe('Test Author');
});

});
