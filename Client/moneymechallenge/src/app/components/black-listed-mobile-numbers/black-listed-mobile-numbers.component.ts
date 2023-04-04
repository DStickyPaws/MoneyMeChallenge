import { Component, OnInit, TemplateRef } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BlacklistMobilenumberServiceService } from 'src/app/services/blacklist-mobilenumber-service.service';
import { IBlacklistedMobileNumber } from 'src/app/interaces/iblacklisted-mobile-number';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-black-listed-mobile-numbers',
  templateUrl: './black-listed-mobile-numbers.component.html',
  styleUrls: ['./black-listed-mobile-numbers.component.scss']
})
export class BlackListedMobileNumbersComponent implements OnInit {
    

    displayedColumns: string[] = [];
    blackListedMobileNumbers : IBlacklistedMobileNumber[] = [];
    dataSource = new MatTableDataSource(this.blackListedMobileNumbers);
    sample : string = '';
    addModalVisibility : boolean = false;
    mobileNumber! : string;

    constructor(private engine : BlacklistMobilenumberServiceService, private snackBar : MatSnackBar){}
    
    ngOnInit(){

      this.getData();
      this.displayedColumns = ['Id','MobileNumber', 'Actions'];
      this.dataSource = new MatTableDataSource(this.blackListedMobileNumbers);

    }

    applyFilter(event: Event) {
      const filterValue = (event.target as HTMLInputElement).value;
      this.dataSource.filter = filterValue.trim().toLowerCase();
    }

    getData()
    {
      this.engine.getAllBlacklistedMobileNumbers().subscribe( 
        response => { 
          console.log(response);
          this.blackListedMobileNumbers = response;
          this.refreshData();
        });      
    }

    refreshData()
    {
      this.dataSource = new MatTableDataSource(this.blackListedMobileNumbers);
      this.snackBar.open("Successful Data Operation.", "DISMISS")
    }

    addModalToggle()
    {      
      this.addModalVisibility = !this.addModalVisibility;
    }

    addData()
    {
      let desiredMobileNumber : string = this.mobileNumber;
      this.engine.addToBlackList({mobilenumber : desiredMobileNumber}).subscribe(
        response => {
          this.snackBar.open("SUCESS!","DIMISS");
          this.getData();
        }
      )
    }

    updateData()
    {

    }

    deleteData(id : number)
    {
      this.engine.unBlackList(id).subscribe(
        response => {
          console.log(response);
        });
        this.snackBar.open("DONE","DIMISS");
        this.getData();
    }
}
