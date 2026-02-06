import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/Services/account-service';
import { LOCATION_UPGRADE_CONFIGURATION } from '@angular/common/upgrade';

@Component({
  selector: 'app-nav',
  imports: [FormsModule],
  templateUrl: './nav.html',
  styleUrl: './nav.css',
})
export class Nav {
  protected creds: any = {}
  private accountService = inject(AccountService) ; 
  protected loggedin = signal(false) ; 

  login(){
    this.accountService.login(this.creds).subscribe({
      next: result => {
        console.log(result) ,
        this.loggedin.set(true)
      },

      error : error => alert(error.message) 

    }) ; 

   
  }

   logout(){
      this.loggedin.set(false) ; 
      this.creds = {}
    }





}
