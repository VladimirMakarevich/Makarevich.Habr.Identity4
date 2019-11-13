import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpApiService } from '../../../http-api.service';
import { LoginModel } from '../models/LoginModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService extends HttpApiService {

  public constructor(
    protected http: HttpClient,
  ) {
    super(http);
  }

  public login = (loginModel: LoginModel): Observable<any> => {
    debugger;
    return this.post<LoginModel, any>(
      'api/account/login', loginModel);
  };

}
