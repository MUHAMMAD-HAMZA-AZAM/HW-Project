export interface IShipper {
  name: string;
  account_number: number;
  phone_number_1: string;
  phone_number_2: Object;
  email: string;
  city: string;
}
export interface IPickup {
  origin: string;
  person_of_contact: string;
  phone_number: string;
  email: string;
  address: string;
}
export interface IConsignee {
  name: string;
  phone_number_1: string;
  phone_number_2: string;
  destination: string;
  email: string;
  address: string;
}
export interface Item {
  order_id: string;
  product_type: string;
  description: string;
  quantity: number;
}
export interface IOrderInformation {
  items: Item[];
  weight: number;
  shipping_mode: string;
  amount: number;
  instructions: Object;
}
export interface ITrackingHistory {
  date_time: string;
  timestamp: number;
  status: string;
  status_reason: Object;
}
export interface IDetails {
  tracking_number: number;
  order_id: string;
  shipper: IShipper;
  pickup: IPickup;
  consignee: IConsignee;
  order_information: IOrderInformation;
  tracking_history: ITrackingHistory[];
}

export interface IOrderTrack {
  details:IDetails[];
}
