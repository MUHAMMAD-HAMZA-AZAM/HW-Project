import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class metaTagsService {
  readonly METATAGS: string[] = ["title", "og:title", "keywords", "description", "og:description", "og:img:alt", "og:url", "conical",]
  constructor(private title: Title, private meta: Meta, @Inject(DOCUMENT) private dom: any, private router: Router) { }
  updateOgUrl(url: string) {
    this.meta.updateTag({ name: 'og:url', content: url })
  }

  updateDescription(desc: string) {
    this.meta.updateTag({ name: 'description', content: desc })
  }
  updateKeyWords(key: string) {
    this.meta.updateTag({ name: 'keywords', content: key })
  }
  updateTitle(title: string) {
    this.title.setTitle(title);
  }
  updateTags(headTitle: string, title: string, desc: string, keywords?: string, ogTitle?: string, ogDescription?: string, canonical?: string) {

    this.updateTitle(headTitle);
    this.setCanonicalURL(canonical)
    let tagObj = {} as IMetaTag;
    for (let i = 0; i < this.METATAGS.length; i++) {
      if (this.METATAGS[i] == "description") {
        tagObj = { name: this.METATAGS[i], content: desc }
      }
      else if (this.METATAGS[i] == "keywords") {
        tagObj = { name: this.METATAGS[i], content: keywords != undefined ? keywords : title }
      }
      else if (this.METATAGS[i] == "og:description") {
        tagObj = { name: this.METATAGS[i], content: ogDescription != undefined ? ogDescription : "" }
      }
      else if (this.METATAGS[i] == "og:title") {
        tagObj = { name: this.METATAGS[i], content: ogTitle != undefined ? ogTitle : "" }
      }


      else if (this.METATAGS[i] == "og:url") {
        tagObj = { name: this.METATAGS[i], content: "https://mall.hoomwork.com" + this.router.url }
      }
      else {
        tagObj = { name: this.METATAGS[i], content: title }
      }
      this.updateTag(tagObj);
    }
  }

  updateTag(metaTag: IMetaTag) {
    //let metaTagType = "";
    let isContain = metaTag.name.includes("og")
    if (!isContain)
      this.meta.updateTag({ name: metaTag.name, content: metaTag.content });
    else
      this.meta.updateTag({ property: metaTag.name, content: metaTag.content });
  }
  setCanonicalURL(url?: string) {
    const canURL = !url ? "https://mall.hoomwork.com" + this.router.url : url;
    const link: HTMLLinkElement = this.dom.createElement('link');
    link.setAttribute('rel', 'canonical');
    this.dom.head.appendChild(link);
    link.setAttribute('href', canURL);
  }

}
interface IMetaTag {
  name: string;
  content: string;
}
