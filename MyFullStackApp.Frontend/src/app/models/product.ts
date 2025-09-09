export interface Product {
  id: number;
  name: string;
  description: string | null; // Coincide con 'string?' en C#
  price: number;
}
