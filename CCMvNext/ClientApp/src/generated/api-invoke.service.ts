import { HttpClient } from '@angular/common/http';

/**
     * This a hand-crafted base class for the generated API Services.
     * Feel free to modify it to adjust behavior of every generated service class.
     */
export class ApiInvokeService {
  constructor(private http: HttpClient, protected controller: string) {
  }

  public getRequest<T>(action: string, params: any): Promise<T> {
    return this.http.get<T>(this.getUrl(action, params), { params: params })
      .toPromise();
  }

  public postRequest<T>(action: string, params: any, data: any): Promise<T> {
    return this.http.post<T>(this.getUrl(action, params), data, { params: params })
      .toPromise();
  }

  public putRequest<T>(action: string, params: any, data: any): Promise<T> {
    return this.http.put<T>(this.getUrl(action, params), data, { params: params })
      .toPromise();
  }

  public deleteRequest<T>(action: string, params: any): Promise<T> {
    return this.http.delete<T>(this.getUrl(action, params), { params: params })
      .toPromise();
  }

  private getUrl(template: string, params: any) {
    // fill route template params.
    if (params) {
      if (template) {
        Object.keys(params).forEach(key => {
          const templateParam = `{${key}}`;
          if (template.indexOf(templateParam) > -1) {
            template = template.replace(templateParam, params[key]);
            delete params[key]; // prevent duplicate params.
          }
        });
      }
    }

    if (template) {
      return `api/${this.controller}/${template}`;
    } else {
      return `api/${this.controller}`;
    }

  }
}
