import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable()
export class AlertifyService {

  constructor() {

  }

  confirm(message: string, okCallback: () => any) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.confirm(message, function (e) {
      if (e) {
        okCallback();
      } else { }
    });
  }

  success(message: string) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.success(message);
  }

  error(message: string) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.error(message);
  }

  warning(message: string) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.warning(message);
  }

  message(message: string) {
    alertify.set('notifier', 'position', 'top-right');
    alertify.message(message);
  }

}
