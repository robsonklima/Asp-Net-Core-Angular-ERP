import { Component } from '@angular/core';
import { Sim } from '@ionic-native/sim';


@Component({
  selector: 'teste-page',
  templateUrl: 'teste.html'
})
export class TestePage {
  
  constructor(
    private sim: Sim
  ) {}

  ionViewWillEnter() {
    this.sim.getSimInfo().then(
      (info) => console.log('Sim info: ', info),
      (err) => console.log('Unable to get sim info: ', err)
    );
    
    this.sim.hasReadPermission().then(
      (info) => console.log('Has permission: ', info)
    );
    
    this.sim.requestReadPermission().then(
      () => console.log('Permission granted'),
      () => console.log('Permission denied')
    );
  }
}