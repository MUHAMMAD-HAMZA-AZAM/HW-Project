
export var StatusCode = {
  OK: 200,
  Error: 505,
  failure: 500,
  Restricted: 403,
  partialContent: 206,
  Conflict: 409
}

export class ResponseVm {
  status: any;
  message: string="";
  resultData: any;
}

export var OrderStatus = {
	Received:1,
	Inprogress:2,
	Delievred:3,
	Completed:4,
	Declined:5,
  PackedAndShipped:6,
  Cancelled : 7
}
