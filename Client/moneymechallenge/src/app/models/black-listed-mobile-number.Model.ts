export class BlackListedMobileNumber {
    Id? : number;
    MobileNumber : string;

    /**
     *
     */
    constructor(MobileNumber : string, Id? : number) {        
       this.Id = Id;
       this.MobileNumber = MobileNumber; 
    }
}
