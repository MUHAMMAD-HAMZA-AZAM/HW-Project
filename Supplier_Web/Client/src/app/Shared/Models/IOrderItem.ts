import { IProduct } from "./IProduct";

export interface OrderItem {
  product: IProduct;
  quantity: number;
}
