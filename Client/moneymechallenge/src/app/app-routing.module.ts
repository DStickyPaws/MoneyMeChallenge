import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AdminViewComponent } from './components/admin-view/admin-view.component';
import { ClientViewComponent } from './components/client-view/client-view.component';

const routes: Routes = [
  { path: 'admin', component: AdminViewComponent },
  { path: 'home', component: HomeComponent },
  { path: 'client', component: ClientViewComponent },
  { path: '', redirectTo:'/home', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
