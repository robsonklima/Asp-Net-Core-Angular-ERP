import { MyApp } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { IonicStorageModule } from '@ionic/storage' 
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';
import { File } from '@ionic-native/file';
import { NgxImageCompressService } from 'ngx-image-compress';
import { HttpModule } from "@angular/http";
import { ChartsModule } from 'ng2-charts';

import { Geolocation } from '@ionic-native/geolocation';
import { Diagnostic } from '@ionic-native/diagnostic'
import { Network } from '@ionic-native/network';
import { AppVersion } from '@ionic-native/app-version';
import { InAppBrowser } from '@ionic-native/in-app-browser';
import { Market } from '@ionic-native/market';
import { AppAvailability } from '@ionic-native/app-availability';
import { LaunchNavigator } from '@ionic-native/launch-navigator';
import { Device } from '@ionic-native/device';
import { BackgroundMode } from '@ionic-native/background-mode';
import { PhonegapLocalNotification } from '@ionic-native/phonegap-local-notification';
import { NativeAudio } from '@ionic-native/native-audio';
import { Vibration } from '@ionic-native/vibration';
import { Badge } from '@ionic-native/badge';
import { Camera } from '@ionic-native/camera';
import { ScreenOrientation } from '@ionic-native/screen-orientation';
import { SignaturePadModule } from 'angular2-signaturepad';
import { BackgroundGeolocation } from '@ionic-native/background-geolocation';
import { AndroidPermissions } from '@ionic-native/android-permissions';

import { DadosGlobaisService } from '../services/dados-globais';
import { ChamadoService } from "../services/chamado";
import { GeolocationService } from './../services/geo-location';
import { UsuarioService } from '../services/usuario';
import { AcaoService } from "../services/acao";
import { DefeitoService } from "../services/defeito";
import { CausaService } from "../services/causa";
import { PecaService } from "../services/peca";
import { TipoServicoService } from "../services/tipo-servico";
import { EquipamentoContratoService } from '../services/equipamento-contrato';
import { HistoricoDetalhePage } from '../pages/historico/historico-detalhe';
import { CheckinCheckoutService } from '../services/checkin-checkout';
import { AjudaTopicoService } from '../services/ajuda-topico';
import { FotoService } from '../services/foto';
import { IndicadorService } from '../services/indicador';
import { LaudoService } from '../services/laudo';
import { MensagemTecnicoService } from '../services/mensagem-tecnico';
import { EquipamentoPOSService } from '../services/equipamento-pos';
import { OperadoraTelefoniaService } from '../services/operadora-telefonia';
import { MotivoComunicacaoService } from '../services/motivo-comunicacao';
import { TipoComunicacaoService } from '../services/tipo-comunicacao';
import { EquipamentoCausaService } from '../services/equipamento-causa';
import { MotivoCancelamentoService } from '../services/motivo-cancelamento';
import { StatusServicoService } from '../services/status-servico';
import { DefeitoPOSService } from '../services/defeito-pos';
import { FerramentaTecnicoService } from '../services/ferramenta-tecnico';
import { FerramentasTecnicoPage } from '../pages/ferramentas-tecnico/ferramentas-tecnico';
import { AcaoCausaService } from '../services/acao-causa';
import { DefeitoCausaService } from '../services/defeito-causa';
import { LogService } from '../services/log';

import { LoadingFactory } from '../factories/loading-factory';

import { CapitalizePipe } from '../pipes/capitalize';
import { EllipsisPipe } from '../pipes/ellipsis';
import { LowercasePipe } from '../pipes/lowercase';

import { LoginPage } from "../pages/login/login";
import { SenhaAlteracaoPage } from "../pages/senha-alteracao/senha-alteracao";
import { HomePage } from '../pages/home/home';
import { HomeMaisOpcoesPage } from '../pages/home/home-mais-opcoes';
import { ChamadosPage } from "../pages/chamados/chamados";
import { ChamadoPage } from "../pages/chamados/chamado";
import { ChamadosFechadosPage } from '../pages/chamados/chamados-fechados';
import { ChamadoFechadoPage } from '../pages/chamados/chamado-fechado';
import { RatDetalhePage } from "../pages/rat-detalhe/rat-detalhe";
import { RatDetalhePecaPage } from "../pages/rat-detalhe-peca/rat-detalhe-peca";
import { HistoricoListaPage } from '../pages/historico/historico-lista';
import { PecasPage } from '../pages/pecas/pecas';
import { PecaPage } from '../pages/peca/peca';
import { AjudaListaPage } from '../pages/ajuda/ajuda-lista';
import { AjudaDetalhePage } from '../pages/ajuda/ajuda-detalhe';
import { ProblemaPage } from '../pages/problema/problema';
import { SobrePage } from '../pages/sobre/sobre';
import { IndicadorMenuPage } from '../pages/indicadores/indicador-menu';
import { IndicadorFiliaisPage } from '../pages/indicadores/indicador-filiais';
import { IndicadorTecnicoPage } from '../pages/indicadores/indicador-tecnico';
import { IndicadorPerformanceTecnicoPage } from '../pages/indicadores/indicador-performance-tecnico';
import { FotosPage } from '../pages/fotos/fotos';
import { LaudoPage } from '../pages/laudos/laudo';
import { SituacaoPage } from '../pages/laudos/situacao';
import { AssinaturaPage } from '../pages/assinatura/assinatura';
import { MensagensPage } from '../pages/mensagens/mensagens';
import { MensagemPage } from '../pages/mensagens/mensagem';
import { TutorialPage } from '../pages/tutorial/tutorial';
import { UsuarioPage } from '../pages/usuario/usuario';
import { AuditoriasPage } from '../pages/auditorias/auditorias';
import { AuditoriaPage } from '../pages/auditorias/auditoria';
import { TestePage } from '../pages/teste/teste';
import { LocalizacaoService } from '../services/localizacao';
import { RatDetalhePosPage } from '../pages/rat-detalhe/rat-detalhe-pos';
import { FotoPage } from '../pages/fotos/foto';
import { ChamadoConfPage } from '../pages/chamados/chamado-conf';
import { LogsPage } from '../pages/logs/logs';
import { AuditoriaService } from '../services/auditoria';
import { PontoPage } from '../pages/ponto/ponto';
import { PontoUsuarioService } from '../services/ponto-usuario';
import { PontosPage } from '../pages/ponto/pontos';
import { PontoDataService } from '../services/ponto-data';
import { ChecklistPreventivaPage } from '../pages/checklists/checklist-preventiva';


@NgModule({
  declarations: [
    MyApp,
    LoginPage,
    SenhaAlteracaoPage,
    HomePage,
    HomeMaisOpcoesPage,
    ChamadosPage,
    ChamadoPage,
    ChamadosFechadosPage,
    ChamadoFechadoPage,
    RatDetalhePage,
    RatDetalhePosPage,
    RatDetalhePecaPage,
    HistoricoListaPage,
    HistoricoDetalhePage,
    PecasPage,
    PecaPage,
    AjudaListaPage,
    AjudaDetalhePage,
    ProblemaPage,
    SobrePage,
    FotosPage,
    FotoPage,
    IndicadorMenuPage,
    IndicadorFiliaisPage,
    IndicadorTecnicoPage,
    IndicadorPerformanceTecnicoPage,
    LaudoPage,
    SituacaoPage,
    MensagensPage,
    MensagemPage,
    AssinaturaPage,
    CapitalizePipe,
    EllipsisPipe,
    LowercasePipe,
    FerramentasTecnicoPage,
    TutorialPage,
    UsuarioPage,
    AuditoriasPage,
    AuditoriaPage,
    LogsPage,
    TestePage,
    ChamadoConfPage,
    PontoPage,
    PontosPage,
    ChecklistPreventivaPage
  ],
  imports: [
    BrowserModule,
    SignaturePadModule,
    HttpModule,
    IonicModule.forRoot(MyApp, {
      monthNames: ['janeiro', 'fevereiro', 'mar\u00e7o', 'abril', 'maio', 'junho', 
      'julho', 'agosto', 'setembro', 'outubro', 'novembro', 'dezembro' ],
      monthShortNames: ['jan', 'fev', 'mar', 'abr', 'mai', 'jun', 'jul', 'ago',
         'set', 'out', 'nov', 'dez' ],
      dayNames: ['domingo', 'segunda-feira', 'ter\u00e7a-feira', 'quarta-feira',
         'quinta-feira', 'sexta-feira', 'sabado' ],
      dayShortNames: ['dom', 'seg', 'ter', 'qua', 'qui', 'sex', 'sab' ],
      scrollAssist: false, 
      autoFocusAssist: false
    }),
    IonicStorageModule.forRoot(),
    ChartsModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    LoginPage,
    SenhaAlteracaoPage,
    HomePage,
    HomeMaisOpcoesPage,
    ChamadosPage,
    ChamadoPage,
    ChamadosFechadosPage,
    ChamadoFechadoPage,
    RatDetalhePage,
    RatDetalhePosPage,
    RatDetalhePecaPage,
    HistoricoListaPage,
    HistoricoDetalhePage,
    PecasPage,
    PecaPage,
    AjudaListaPage,
    AjudaDetalhePage,
    ProblemaPage,
    SobrePage,
    FotosPage,
    FotoPage,
    IndicadorMenuPage,
    IndicadorFiliaisPage,
    IndicadorTecnicoPage,
    IndicadorPerformanceTecnicoPage,
    LaudoPage,
    SituacaoPage,
    MensagensPage,
    MensagemPage,
    AssinaturaPage,
    FerramentasTecnicoPage,
    TutorialPage,
    UsuarioPage,
    AuditoriasPage,
    AuditoriaPage,
    ChamadoConfPage,
    PontoPage,
    PontosPage,
    LogsPage,
    ChecklistPreventivaPage,
    TestePage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    { provide: ErrorHandler, useClass: IonicErrorHandler },
    Diagnostic,
    Geolocation,
    BackgroundGeolocation,
    BackgroundMode,
    LaunchNavigator,
    PhonegapLocalNotification,
    AndroidPermissions,
    Badge,
    NativeAudio,
    Vibration,
    Network,
    AppVersion,
    InAppBrowser,
    NgxImageCompressService,
    Market,
    AppAvailability,
    Device,
    Camera,
    File,
    ScreenOrientation,
    DadosGlobaisService,
    ChamadoService,
    GeolocationService,
    UsuarioService,
    AcaoService,
    DefeitoService,
    CausaService,
    PecaService,
    PontoUsuarioService,
    PontoDataService,
    EquipamentoContratoService,
    TipoServicoService,
    CheckinCheckoutService,
    FotoService,
    AjudaTopicoService,
    IndicadorService,
    LaudoService,
    MensagemTecnicoService,
    EquipamentoPOSService,
    OperadoraTelefoniaService,
    MotivoComunicacaoService,
    MotivoCancelamentoService,
    TipoComunicacaoService,
    EquipamentoCausaService,
    StatusServicoService,
    DefeitoPOSService,
    FerramentaTecnicoService,
    AcaoCausaService,
    DefeitoCausaService,
    LocalizacaoService,
    AuditoriaService,
    LogService,
    LoadingFactory
  ]
})
export class AppModule {}