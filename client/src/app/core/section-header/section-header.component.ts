import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Breadcrumb } from 'src/app/shared/models/breadcrumb';
import { BreadcrumbService } from '../breadcrumb.service';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})
export class SectionHeaderComponent implements OnInit {
  public breadcrumbs$: Observable<Breadcrumb[]>;

  constructor(private bService: BreadcrumbService) {}

  ngOnInit(): void {
    this.breadcrumbs$ = this.bService.breadcrumbs$;
  }

}
