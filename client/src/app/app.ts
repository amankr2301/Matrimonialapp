import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, Signal, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";

@Component({
  selector: 'app-root',
  imports: [Nav],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
  
  private http = inject(HttpClient) ; 
  protected title = 'Matrimonial app';
  protected members = signal<any>([]) ; 

  async ngOnInit() {
    this.members.set(await this.getMembers()) ; 
    
  }

  async getMembers(){   

    try {
      return lastValueFrom(this.http.get('/api/members')
)
      
    } catch (error) {
      console.log(error) ;
      throw error ;  
      
    }
    

  }
  

}
