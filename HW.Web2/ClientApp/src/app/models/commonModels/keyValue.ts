export interface keyValue<T, U> {
  text: T;
  value: U;
}

export interface keyValueAutoComplete<T, U> {
  value: T;
  id: U;
}

export interface keyValueMulti<T, U> {
  text: T;
  value: U;
  selectedValue: any;
}

export interface keyValueCustom<T, U> {
  key: T;
  value: U;
}
