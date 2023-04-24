import { Injectable } from '@angular/core';
import { CommonService } from '../HttpClient/_http';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot, Params, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubSkillsResolverService implements Resolve<any> {
  public id: string="";

  constructor(private router: Router, private common: CommonService, private route: ActivatedRoute, ) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    
    this.id = route.data['id'];
    return this.common.get(this.common.apiRoutes.Tradesman.GetSkillTagsById + "?SkillId=" + 10045).map(data => {
      return data;
    });
  }
}
