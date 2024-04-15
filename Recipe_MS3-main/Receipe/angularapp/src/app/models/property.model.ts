// src/app/models/property.model.ts

export interface Property {
    propertyId: number;
    name: string;
    description: string;
    address: string;
    propertyType: string;
    bedrooms: number;
    bathrooms: number;
    monthlyRent: number;
    available: boolean;
    createdAt: Date;
  }
  