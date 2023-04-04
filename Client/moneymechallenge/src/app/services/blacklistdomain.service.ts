import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

const EndPoint : string = "http://localhost:5109";

@Injectable({
  providedIn: 'root'
})
export class BlacklistdomainService {

  constructor(private httpClient : HttpClient) { }

  getAllBlacklistedDomain() {    
    return this.httpClient.get<IBlacklistedMobileNumber[]>(EndPoint + "/BlacklistedDomain/GetBlacklistedDomains");
  }

  addToBlackList(mobileNumber : IBlacklistedMobileNumber)
  {
    return this.httpClient.post(EndPoint + "/BlacklistedMobilenumber/AddToMobilenumberBlacklist",{ "mobilenumber" : mobileNumber.mobilenumber });
  }

  unBlackList(id : number)
  {
    return this.httpClient.delete(EndPoint + "/BlacklistedMobilenumber/RemoveFromBlacklistById?id=" + id, {});
  }
}
