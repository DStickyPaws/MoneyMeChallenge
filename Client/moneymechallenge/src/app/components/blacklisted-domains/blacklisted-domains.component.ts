import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BlacklistdomainService } from 'src/app/services/blacklistdomain.service';

@Component({
  selector: 'app-blacklisted-domains',
  templateUrl: './blacklisted-domains.component.html',
  styleUrls: ['./blacklisted-domains.component.scss']
})
export class BlacklistedDomainsComponent {

  addModalVisibility : boolean = false;

  constructor(private engine : BlacklistdomainService, private snackBar : MatSnackBar){}

  addModalToggle()
  {      
    this.addModalVisibility = !this.addModalVisibility;
  }
}
