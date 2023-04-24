export class keyValue<T, U> {
  text: T;
  value: U;
}

export class keyValueAutoComplete<T, U> {
  value: T;
  id: U;
}

export class keyValueMulti<T, U> {
  text: T;
  value: U;
  selectedValue: any;
}

export class keyValueCustom<T, U> {
  key: T;
  value: U;
}
