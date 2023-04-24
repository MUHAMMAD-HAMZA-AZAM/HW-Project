import { Injectable } from '@angular/core';
import { CommonService } from '../HttpClient/_http';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot, Params, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class SkillsmetatagsResolverService implements Resolve<any> {
  constructor(private router: Router, private service: CommonService, private cRoute: ActivatedRoute, ) {
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
    return this.service.get(this.service.apiRoutes.Tradesman.GetSkillTagsById + "?skillId=" + route.params['id'])
  }
}
