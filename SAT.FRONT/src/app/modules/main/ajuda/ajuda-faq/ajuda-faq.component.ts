import { Component, LOCALE_ID, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { FaqCategory } from 'app/core/types/ajuda.types';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-ajuda-faq',
  templateUrl: './ajuda-faq.component.html',
  styleUrls: ['./ajuda-faq.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})

export class AjudaFaqComponent implements OnInit
{
  faqCategories: FaqCategory[];
  private _unsubscribeAll: Subject<any> = new Subject();

  /**
   * Constructor
   */
  constructor ()
  {
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Lifecycle hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * On init
   */
  ngOnInit(): void
  {
    // Get the FAQs
    // this._helpCenterService.faqs$
    //   .pipe(takeUntil(this._unsubscribeAll))
    //   .subscribe((faqCategories) =>
    //   {
    //     this.faqCategories = faqCategories;
    //   });
  }

  /**
   * On destroy
   */
  ngOnDestroy(): void
  {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Track by function for ngFor loops
   *
   * @param index
   * @param item
   */
  trackByFn(index: number, item: any): any
  {
    return item.id || index;
  }
}
