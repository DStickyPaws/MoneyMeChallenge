import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBlacklistedMobileNumber } from '../interaces/iblacklisted-mobile-number';

const EndPoint : string = "http://localhost:5109";

@Injectable({
  providedIn: 'root'
})
export class BlacklistMobilenumberServiceService {

  constructor(private httpClient : HttpClient) { }

  getAllBlacklistedMobileNumbers() {    
    return this.httpClient.get<IBlacklistedMobileNumber[]>(EndPoint + "/BlacklistedMobilenumber/GetAllBlacklistedMobilenumbers");
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
