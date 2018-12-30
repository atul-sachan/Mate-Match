import { environment } from 'src/environments/environment';

export class Constants {



  constructor() { }

  // tslint:disable-next-line:max-line-length
  static blacklistedRoutes: string[] = (!environment.production) ? ['http://localhost:5000/api/auth'] : ['https://mate-match-api.azurewebsites.net/api/auth'];
  static whiteListDomains: string[] = (!environment.production) ? ['localhost:5000'] : ['mate-match-api.azurewebsites.net'];

  static apiUrl(): string {
    if (!environment.production) {
      return 'http://localhost:5000/api/';
    } else {
      return 'https://mate-match-api.azurewebsites.net/api/';
    }
  }


}
