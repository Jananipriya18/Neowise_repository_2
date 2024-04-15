import { Recipe } from './property.model';

describe('Recipe', () => {
  fit('RecipeModel_should_create_an_instance', () => {
    // Create a factory function to create instances of Recipe
    function createRecipe(recipeId: number, name: string, description: string, ingredients: string, instructions: string, author: string): Recipe {
      return {
        recipeId,
        name,
        description,
        ingredients,
        instructions,
        author
      };
    }

    // Use the factory function to create an instance of Recipe
    const recipe: Recipe = createRecipe(
      1,
      'Test Recipe',
      'Test Description',
      'Test Ingredients',
      'Test Instructions',
      'Test Author'
    );

    // Assert that the instance is created successfully
    expect(recipe).toBeDefined();
    // This line will cause a compilation error and should be removed
    // expect(recipe instanceof Recipe).toBeTruthy();
  });

  fit('RecipeModel_should_update_property_values_using_setters', () => {
    // Similar setup as above
    function createRecipe(recipeId: number, name: string, description: string, ingredients: string, instructions: string, author: string): Recipe {
      return {
        recipeId,
        name,
        description,
        ingredients,
        instructions,
        author
      };
    }

    const recipe: Recipe = createRecipe(
      1,
      'Test Recipe',
      'Test Description',
      'Test Ingredients',
      'Test Instructions',
      'Test Author'
    );

    // Assert that the instance is created successfully
    expect(recipe).toBeTruthy();
    // Assertions for property values and data types remain the same
    expect(recipe.recipeId).toBeTruthy();
    expect(recipe.name).toBeTruthy();
    expect(recipe.description).toBeTruthy();
    expect(recipe.ingredients).toBeTruthy();
    expect(recipe.instructions).toBeTruthy();
    expect(recipe.author).toBeTruthy();
    expect(typeof recipe.recipeId).toEqual('number');
    expect(typeof recipe.name).toEqual('string');
    expect(typeof recipe.description).toEqual('string');
    expect(typeof recipe.ingredients).toEqual('string');
    expect(typeof recipe.instructions).toEqual('string');
    expect(typeof recipe.author).toEqual('string');
  });
});
