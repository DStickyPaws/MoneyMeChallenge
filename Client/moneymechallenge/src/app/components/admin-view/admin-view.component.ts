import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
})
export class AdminViewComponent {

  isBlacklistMobileVisible : boolean = false;
  isBlacklistDomainsVisible : boolean = false;
  constructor() {}

  toggleBlacklistMobileNumbers()
  {
    this.isBlacklistMobileVisible = !this.isBlacklistMobileVisible;
  }

  toggleBlacklistDomains()
  {
    this.isBlacklistDomainsVisible = !this.isBlacklistDomainsVisible;
  }
}
