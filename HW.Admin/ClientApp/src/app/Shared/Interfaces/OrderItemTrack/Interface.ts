
export interface IShipper {
  name: string;
  account_number: number;
  phone_number_1: string;
  phone_number_2: string;
  address: string;
  origin: string;
}

export interface IConsignee {
  name: string;
  phone_number_1: string;
  phone_number_2: Object;
  destination: string;
  address: string;
}

export interface Item {
  product_type: string;
  description: string;
  quantity: number;
}

export interface IOrderInformation {
  items: Item[];
  weight: number;
  instructions: string;
}

export interface ITrackingHistory {
  date_time: string;
  status: string;
  status_reason: Object;
}

export interface IDetails {
  tracking_number: string;
  shipper: IShipper;
  consignee: IConsignee;
  order_information: IOrderInformation;
  tracking_history: ITrackingHistory[];
}
