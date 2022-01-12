import { Component, OnInit, AfterViewInit } from '@angular/core';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import * as moment from 'moment'
declare var JitsiMeetExternalAPI: any;

@Component({
  selector: 'app-jitsi',
  templateUrl: './jitsi.component.html'
})
export class JitsiComponent implements OnInit, AfterViewInit {

  domain: string = "meet.jit.si";
  room: any;
  options: any;
  api: any;
  user: any;
  sessionData: UserSession;

  isAudioMuted = false;
  isVideoMuted = false;

  constructor(
    private _userSvc: UserService
  ) {
    this.sessionData = JSON.parse(this._userSvc.userSession);
  }

  ngOnInit(): void {
    this.room = `SAT_${this.sessionData.usuario.codUsuario}_${moment().format('yyyyMMDDHHmmsss')}`
    this.user = {
      name: 'Equipe SAT'
    }
  }

  ngAfterViewInit(): void {
    this.options = {
      roomName: this.room,
      width: 900,
      height: 500,
      configOverwrite: { prejoinPageEnabled: false },
      interfaceConfigOverwrite: {
        TOOLBAR_BUTTONS: [],
      },
      parentNode: document.querySelector('#jitsi-iframe'),
      userInfo: {
        displayName: this.user.name
      }
    }

    this.api = new JitsiMeetExternalAPI(this.domain, this.options);

    this.api.addEventListeners({
      readyToClose: this.handleClose,
      participantLeft: this.handleParticipantLeft,
      participantJoined: this.handleParticipantJoined,
      videoConferenceJoined: this.handleVideoConferenceJoined,
      videoConferenceLeft: this.handleVideoConferenceLeft,
      audioMuteStatusChanged: this.handleMuteStatus,
      videoMuteStatusChanged: this.handleVideoStatus,
    });
  }


  handleClose = () => {
    console.log("handleClose");
  }

  handleParticipantLeft = async (participant) => {
    console.log("handleParticipantLeft", participant);
    const data = await this.getParticipants();
  }

  handleParticipantJoined = async (participant) => {
    console.log("handleParticipantJoined", participant);
    const data = await this.getParticipants();
  }

  handleVideoConferenceJoined = async (participant) => {
    console.log("handleVideoConferenceJoined", participant);
    const data = await this.getParticipants();
  }

  handleVideoConferenceLeft = () => {
    console.log("handleVideoConferenceLeft");
  }

  handleMuteStatus = (audio) => {
    console.log("handleMuteStatus", audio);
  }

  handleVideoStatus = (video) => {
    console.log("handleVideoStatus", video);
  }

  getParticipants() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(this.api.getParticipantsInfo());
      }, 500)
    });
  }

  executeCommand(command: string) {
    this.api.executeCommand(command);
    
    if (command == 'toggleAudio') {
      this.isAudioMuted = !this.isAudioMuted;
    }

    if (command == 'toggleVideo') {
      this.isVideoMuted = !this.isVideoMuted;
    }
  }
}