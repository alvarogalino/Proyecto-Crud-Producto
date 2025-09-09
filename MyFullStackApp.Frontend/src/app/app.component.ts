import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Product } from './models/product';
import { ProductService } from './services/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'MyFullStackApp.Frontend';
  productForm: FormGroup;
  products: Product[] = [];
  editingProduct: Product | null = null;

constructor(
    private fb: FormBuilder,
    private productService: ProductService
  ){
    // Inicialización del formulario reactivo
    this.productForm = this.fb.group({
      id: [0], // Lo usamos para la edición, pero no se muestra en el formulario
      name: ['', Validators.required],
      description: [''],
      price: [0, [Validators.required, Validators.min(0.01)]]
    });
  }
   ngOnInit(): void {
    this.loadProducts(); // Cargar productos al iniciar el componente
  }
  loadProducts(): void {
    this.productService.getProducts().subscribe(
      (data) => {
        this.products = data;
      },
      (error) => {
        console.error('Error al cargar productos:', error);
      }
    );
  }

    // Maneja el envío del formulario (crear o actualizar)
  onSubmit(): void {
    if (this.productForm.valid) {
      const product: Product = this.productForm.value;

      if (this.editingProduct) {
        // Si estamos editando, llamamos al método de actualización
        this.productService.updateProduct(product.id, product).subscribe(
          () => {
            console.log('Producto actualizado con éxito');
            this.loadProducts(); // Recargar la lista
            this.resetForm(); // Limpiar y resetear el formulario
          },
          (error) => {
            console.error('Error al actualizar producto:', error);
          }
        );
      } else {
        // Si no estamos editando, creamos un nuevo producto
        this.productService.createProduct(product).subscribe(
          () => {
            console.log('Producto creado con éxito');
            this.loadProducts(); // Recargar la lista
            this.resetForm(); // Limpiar y resetear el formulario
          },
          (error) => {
            console.error('Error al crear producto:', error);
          }
        );
      }
    } else {
      console.error('El formulario no es válido');
      // Opcional: Marcar todos los campos como "touched" para mostrar errores de validación
      this.productForm.markAllAsTouched();
    }
  }

   // Carga los datos de un producto en el formulario para edición
  editProduct(product: Product): void {
    this.editingProduct = product;
    this.productForm.patchValue(product); // Llenar el formulario con los datos del producto
  }

  // Elimina un producto
  deleteProduct(id: number): void {
    if (confirm('¿Estás seguro de que quieres eliminar este producto?')) {
      this.productService.deleteProduct(id).subscribe(
        () => {
          console.log('Producto eliminado con éxito');
          this.loadProducts(); // Recargar la lista
        },
        (error) => {
          console.error('Error al eliminar producto:', error);
        }
      );
    }
  }

  // Resetea el formulario y el estado de edición
  resetForm(): void {
    this.productForm.reset();
    this.editingProduct = null;
    // Asegurarse de que el ID se resetee a 0 o null si es necesario para nuevas creaciones
    this.productForm.get('id')?.setValue(0);
  }

}
