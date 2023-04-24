import { Injectable } from "@angular/core"
import { IProduct } from "../Models/IProduct"
import { BehaviorSubject, Observable } from "rxjs"
import { OrderItem } from "../Models/IOrderItem"

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private subject = new BehaviorSubject<OrderItem[]>([]);
  private orderItems$: Observable<OrderItem[]> = this.subject.asObservable();
  public bulkInventory = new Array();
  constructor() {
  }

  getItems(): Observable<OrderItem[]> {
    return this.orderItems$;
  }
  populateCart(orderItems:any){
    this.subject.next(orderItems);
  }
  addItem(orderItem: OrderItem, bulkInventorybyVarient:any) {
    const orderItems = this.subject.getValue();
    this.bulkInventory = bulkInventorybyVarient;
    const productIndex = orderItems.findIndex(item => item.product.variantId === orderItem.product.variantId && item.product.id === orderItem.product.id); //find by product id
    if (productIndex >= 0)
    {
      const updatedOrderItem = orderItems[productIndex];
      updatedOrderItem.quantity += orderItem.quantity;
      for (let items of this.bulkInventory)
      {
        if (items.minQuantity <= updatedOrderItem.quantity && updatedOrderItem.quantity <= items.maxQuantity)
        {
          updatedOrderItem.product.discountedPrice = items.bulkPrice;
        }
      }
      const newOrderItems = orderItems.slice(0);
      newOrderItems[productIndex] = {...orderItems[productIndex],...updatedOrderItem}
    } else {
      orderItems.push(orderItem)
    }
    this.subject.next(orderItems);
    localStorage.setItem("ca_items", JSON.stringify(orderItems));
  }
  removeItem(removedItem: OrderItem) {
    const orderItems = this.subject.getValue();
    const productIndex = orderItems.findIndex(item => item.product.variantId === removedItem.product.variantId && item.product.id === removedItem.product.id); //find by product id
    if (productIndex >= 0) {
      orderItems.splice(productIndex, 1);
      this.subject.next(orderItems);
    }
    localStorage.setItem("ca_items", JSON.stringify(orderItems));
  }
  changeItemQuantity(orderItem: OrderItem, quantity:number)
  {
    const orderItems = this.subject.getValue();
    const productIndex = orderItems.findIndex(item => item.product.variantId === orderItem.product.variantId && item.product.id === orderItem.product.id); //find by product id
    if (productIndex >= 0)
    {
      orderItems[productIndex].quantity = quantity;
      orderItems[productIndex].product.discountedPrice = orderItem.product.discountedPrice;
      this.subject.next(orderItems);
    }
    localStorage.setItem("ca_items", JSON.stringify(orderItems));
  }
  clearCartItems() {
    const items = new Array<OrderItem>();
    this.subject.next(items);
    localStorage.setItem("ca_items", JSON.stringify(items));
  }
}
