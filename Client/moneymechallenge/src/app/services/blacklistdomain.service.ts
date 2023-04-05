import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBlacklistedDomain } from '../interaces/iblacklisted-domain';

const EndPoint : string = "http://localhost:5109";

@Injectable({
  providedIn: 'root'
})
export class BlacklistdomainService {

  constructor(private httpClient : HttpClient) { }

  getAllBlacklistedDomain() {    
    return this.httpClient.get<IBlacklistedDomain[]>(EndPoint + "/BlacklistedDomain/GetBlacklistedDomains");
  }

  addToBlackList(domain : IBlacklistedDomain)
  {
    return this.httpClient.post(EndPoint + "/BlacklistedDomain/BlacklistDomain",{ "EmailDomain" : domain.EmailDomain });
  }

  unBlackList(id : number)
  {
    return this.httpClient.delete<string>(EndPoint + "/BlacklistedDomain/RemoveFromBlacklistById?Id=" + id, {});
  }
}
