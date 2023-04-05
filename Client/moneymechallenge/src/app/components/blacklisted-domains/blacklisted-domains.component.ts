import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { IBlacklistedDomain } from 'src/app/interaces/iblacklisted-domain';
import { BlacklistdomainService } from 'src/app/services/blacklistdomain.service';

@Component({
  selector: 'app-blacklisted-domains',
  templateUrl: './blacklisted-domains.component.html',
  styleUrls: ['./blacklisted-domains.component.scss']
})
export class BlacklistedDomainsComponent implements OnInit {

  displayedColumns: string[] = [];
  blacklistedDomains: IBlacklistedDomain[] = [];
  dataSource = new MatTableDataSource(this.blacklistedDomains);
  addModalVisibility: boolean = false;
  domain!: string;

  constructor(private engine: BlacklistdomainService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.getData();
    this.displayedColumns = ['Id', 'EmailDomains', 'Actions'];
    this.dataSource = new MatTableDataSource(this.blacklistedDomains);
  }

  getData() {
    this.engine.getAllBlacklistedDomain().subscribe(
      response => {
        console.log(response);
        this.blacklistedDomains = response;
        this.refreshData();
      });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  addModalToggle() {
    this.addModalVisibility = !this.addModalVisibility;
  }

  addData() {
    let desiredDomain: string = this.domain;
    this.engine.addToBlackList({ EmailDomain: desiredDomain }).subscribe(
      response => {
        this.snackBar.open("SUCESS!", "DIMISS");
        this.getData();
      }
    )
  }

  refreshData() {
    this.dataSource = new MatTableDataSource(this.blacklistedDomains);
    this.snackBar.open("Successful Data Operation.", "DISMISS")
  }

  // updateData() {
  // }

  deleteData(id: number) {
    this.engine.unBlackList(id).subscribe(
      response => {
        console.log(response);
      });
    this.snackBar.open("DONE", "DIMISS");
    this.getData();
  }
}
