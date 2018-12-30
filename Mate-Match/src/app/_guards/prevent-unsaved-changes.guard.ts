import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

import { MemberEditComponent } from '../members/member-edit/member-edit.component';

// Consider using this interface for all CanDeactivate guards,
// and have your components implement this interface, too.
//
//   e.g. export class CanDeactivateGuard implements CanDeactivate<CanComponentDeactivate> {
//
// export interface CanComponentDeactivate {
// canDeactivate: () => any;
// }

@Injectable({ providedIn: 'root' })
export class PreventUnsavedChange implements CanDeactivate<MemberEditComponent> {
  // tslint:disable-next-line:max-line-length
  canDeactivate(component: MemberEditComponent, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    // if (component.editForm.dirty) {
    //   return confirm('Are you sure want to continue?  Any unused changes will be lost ');
    // }
    return true;
  }
}
