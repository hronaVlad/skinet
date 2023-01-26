export class Breadcrumb {
    label?: string;
    url: string;
    meta?: BreadcrumbMeta
  }

export class BreadcrumbMeta {
  id?: number;
  alias: string;
  function?: any;
}  
  