import { Laptop } from './laptop.model';

describe('Laptop', () => {
  fit('Models_should_create_laptop_instance', () => {
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
