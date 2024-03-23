#include <advanlys.h>
#include <cvirte.h>		
#include <utility.h>
#include <formatio.h>
#include <ansi_c.h>
#include <userint.h>
#include "ProiectAPD.h"


#define SAMPLE_RATE		0
#define NPOINTS			1

static int panelHandle;
static int freqPanel = 0; //pentru freqpanel
int waveInfo[2]; //waveInfo[0] = sampleRate				 //waveInfo[1] = number of elements
double sampleRate = 0.0;
int npoints = 0;
double *waveData = 0;
int filtru_semnal;
int n_mediere;
double *filtered=0;
double alpha=0.0;
int start,stop;
int hist[100];
int interv=10;
double axis[100];
BOOL AFISAREANVELOPA=0;
double *anvelopa=0;
int N=0;//nr de puncte ales de pe interfata


int main (int argc, char *argv[])
{
	if (InitCVIRTE (0, argv, 0) == 0)
		return -1;	/* out of memory */
	if ((panelHandle = LoadPanel (0, "ProiectAPD.uir", mainPanel)) < 0)
		return -1;
	if ((	freqPanel = LoadPanel (0, "ProiectAPD.uir", FREQ_PANEL))<0)
		return -1;
	
	DisplayPanel (panelHandle);
	RunUserInterface ();
	DiscardPanel (panelHandle);
	return 0;
}


int CVICALLBACK OnMainPanel (int panel, int event, void *callbackData,
							 int eventData1, int eventData2)
{
	switch (event)
	{
		case EVENT_GOT_FOCUS:

			break;
		case EVENT_LOST_FOCUS:

			break;
		case EVENT_CLOSE:
			QuitUserInterface(0);
			break;
	}
	return 0;
}



int CVICALLBACK OnFreqPanel (int panel, int event, void *callbackData,
							 int eventData1, int eventData2)
{
	switch (event)
	{
		case EVENT_GOT_FOCUS:

			break;
		case EVENT_LOST_FOCUS:

			break;
		case EVENT_CLOSE:
			QuitUserInterface (0);
			break;
	}
	return 0;
}






int CVICALLBACK OnLoadCB (int panel, int control, int event,
						  void *callbackData, int eventData1, int eventData2)
{	
	double skew, kurtosis;
	switch (event)
	{
		case EVENT_COMMIT:
			//executa script python pentru conversia unui fisierului .wav in .txt
			//LaunchExecutable("python main.py");
			
			//astept sa fie generate cele doua fisiere (modificati timpul daca este necesar
			//Delay(4);
			
			//incarc informatiile privind rata de esantionare si numarul de valori
			FileToArray("wafeInfo.txt", waveInfo, VAL_INTEGER, 2, 1, VAL_GROUPS_TOGETHER, VAL_GROUPS_AS_COLUMNS, VAL_ASCII);
			sampleRate = waveInfo[SAMPLE_RATE];
			npoints = waveInfo[NPOINTS];
			
			if( sampleRate*10 < npoints)
			{
				npoints=sampleRate*6;
			}
			
			//alocare memorie pentru numarul de puncte
			waveData = (double *) calloc(npoints, sizeof(double));
			filtered=(double *)calloc(npoints,sizeof(double));
			
			//incarcare din fisierul .txt in memorie (vector)
			FileToArray("waveData.txt", waveData, VAL_DOUBLE, npoints, 1, VAL_GROUPS_TOGETHER, VAL_GROUPS_AS_COLUMNS, VAL_ASCII);
			
			//afisare pe grapf
			PlotY(panel, mainPanel_RAWGRAPH, waveData, npoints, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_RED);
			
			DeleteGraphPlot(freqPanel,FREQ_PANEL_GRAPH_RAW_DATA,-1, VAL_IMMEDIATE_DRAW); 
			PlotY(freqPanel,FREQ_PANEL_GRAPH_RAW_DATA, waveData, npoints, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_RED);
			
			SetCtrlAttribute(panelHandle,mainPanel_BINARYSWITCH_2,ATTR_DIMMED,1);
			Moment(waveData,256,3,&skew);
			SetCtrlVal (panelHandle, mainPanel_NUMERIC_10, skew);
			Moment(waveData,256,4,&kurtosis);
			SetCtrlVal (panelHandle, mainPanel_NUMERIC_11, kurtosis);
			
			
			
			
			break;
	}
	return 0;
}




int CVICALLBACK OnMaxMin (int panel, int control, int event,
						  void *callbackData, int eventData1, int eventData2)
{
	double minValue, maxValue;
	int maxIndex, minIndex;
	double mean = 0.0;
	double median=0.0;
	double dispersie=0.0;
			
	switch (event)
	{
		case EVENT_COMMIT:
			
			MaxMin1D (waveData, npoints, &maxValue, &maxIndex, &minValue, &minIndex);
			SetCtrlVal(panelHandle, mainPanel_NUMERIC_3, minValue);
			SetCtrlVal(panelHandle, mainPanel_NUMERIC_2, maxValue);
			SetCtrlVal(panelHandle, mainPanel_NUMERIC_4, maxIndex);
			SetCtrlVal(panelHandle, mainPanel_NUMERIC_5, minIndex);
			
			Mean(waveData,npoints,&mean);
			SetCtrlVal(panel,mainPanel_NUMERIC,mean);
			
			Median(waveData,npoints,&median);
			SetCtrlVal(panel,mainPanel_NUMERIC_6,median);
			Variance(waveData,npoints,&median,&dispersie);
			SetCtrlVal(panel,mainPanel_NUMERIC_7,dispersie);
			
			
			Histogram(waveData,npoints,minValue-1,maxValue+1,hist,axis,interv);
			DeleteGraphPlot (panel,mainPanel_HiISTOGRAM_GRAPH, -1, VAL_DELAYED_DRAW);
            PlotXY (panel,mainPanel_HiISTOGRAM_GRAPH , axis,  hist, interv, VAL_DOUBLE, VAL_SSIZE_T, VAL_VERTICAL_BAR, VAL_EMPTY_SQUARE, VAL_SOLID, 1, VAL_GREEN); 
			break;
			
	}
	return 0;
}

int CVICALLBACK OnAplica (int panel, int control, int event,
						  void *callbackData, int eventData1, int eventData2)
{	
	
	int zeroCross=0;
	switch (event)
	{
		case EVENT_COMMIT:
		
			
				SetCtrlVal(panel,mainPanel_START,0);
				SetCtrlVal(panel,mainPanel_STOP,1);
				
				for (int i=0;i<npoints-1;i++){
					if(((waveData[i]<0)&&(waveData[i+1]>0))||((waveData[i]>0)&&(waveData[i+1]<0)))
						zeroCross++;
				}
				SetCtrlVal(panel,mainPanel_NUMERIC_8,zeroCross);
				
				GetCtrlVal(panel,mainPanel_BINARYSWITCH,&filtru_semnal);
				DeleteGraphPlot(panelHandle,mainPanel_FILTEREDGRAPH,-1,VAL_IMMEDIATE_DRAW);
				if(filtru_semnal){
					//int n_mediere;
					double sum=0.0;
					GetCtrlVal(panelHandle,mainPanel_BINARYSWITCH_2,&n_mediere);
					for(int i=0;i<n_mediere;i++)
						sum+=waveData[i];
					for(int i=0;i<n_mediere;i++)
						filtered[i]=sum/n_mediere;
					for(int i=n_mediere;i<npoints;i++)
					{
						sum-=waveData[i-n_mediere];
						sum+=waveData[i];
						filtered[i]=sum/n_mediere;
					}
					
				}
				else if (!filtru_semnal)
				{
					GetCtrlVal(panel,mainPanel_NUMERIC_9,&alpha);
				
					filtered[0]=waveData[0];
					for(int i=1;i<npoints;i++)
						filtered[i]=(1-alpha)*filtered[i-1]+alpha*waveData[i];
				}
				
				PlotY(panel,mainPanel_FILTEREDGRAPH,filtered, npoints, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS,VAL_RED);
			
		
			
			
			break;
			
	}
	return 0;
}



int CVICALLBACK OnFiltruCB (int panel, int control, int event,
							void *callbackData, int eventData1, int eventData2)
{
	int val;
	switch (event)
	{
		case EVENT_COMMIT:
			GetCtrlVal(panelHandle,mainPanel_BINARYSWITCH,&val);
			if(val==0)
			{
				SetCtrlAttribute(panelHandle,mainPanel_NUMERIC_9,ATTR_DIMMED,0);
				SetCtrlAttribute(panelHandle,mainPanel_BINARYSWITCH_2,ATTR_DIMMED,1);
			}
			else
			{
				SetCtrlAttribute(panelHandle,mainPanel_NUMERIC_9,ATTR_DIMMED,1);
				SetCtrlAttribute(panelHandle,mainPanel_BINARYSWITCH_2,ATTR_DIMMED,0);
			}
			break;
	}
	return 0;
}

int CVICALLBACK OnSaveButton (int panel, int control, int event,
							  void *callbackData, int eventData1, int eventData2)
{
	
		int tip;
		int dim;
		double alpha;
		int image;
		char filename[256] = {0},filename1[256]={0},filename2[256]={0};
		
	switch (event)
	{																									 
		case EVENT_COMMIT:
			  
			  //numele cu care o sa salvam imaginea
			  GetCtrlVal(panel,mainPanel_START,&start);
			  GetCtrlVal(panel,mainPanel_STOP,&stop);
			  GetCtrlVal(panel,mainPanel_BINARYSWITCH_2,&dim);
			  GetCtrlVal(panel,mainPanel_NUMERIC_9,&alpha);
			  GetCtrlVal(panel,mainPanel_BINARYSWITCH,&tip);
			  
			  if(AFISAREANVELOPA==0){
			  	sprintf(filename,"grafic_row_data_secundele_%d_%d.jpg",start,stop);
			  }
			  else{
				sprintf(filename,"grafic_anvelopa_semnal_secundele_%d_%d.jpg",start,stop);
			  	 }
			 GetCtrlDisplayBitmap(panel,mainPanel_RAWGRAPH,1,&image);
			 SaveBitmapToJPEGFile(image,filename,JPEG_PROGRESSIVE,100);
			 DiscardBitmap(image);
			  
			  if(tip==0)
			  {
				 
				  sprintf(filename1," grafic_filter_data_ordin_1_alpha_%f_secundele_%d_%d.jpg",alpha,start,stop);  
			  }
			  else
			  {
				 sprintf(filename1,"grafic_filter_data_mediere_dim_%d_secundele_%d_%d.jpg",dim,start,stop);    
			  }
			  GetCtrlDisplayBitmap(panel,mainPanel_FILTEREDGRAPH,1,&image);
			  SaveBitmapToJPEGFile(image,filename1,JPEG_PROGRESSIVE,100);
			  DiscardBitmap(image);
			  
			  sprintf(filename2,"grafic_histogram_%d_%d.jpg",start,stop);
			  GetCtrlDisplayBitmap(panel,mainPanel_HiISTOGRAM_GRAPH,1,&image);
			  SaveBitmapToJPEGFile(image,filename2,JPEG_PROGRESSIVE,100);
			  DiscardBitmap(image);

			break;
	}
	return 0;
}


int CVICALLBACK Generare_Anvelopa (int panel, int control, int event,
								   void *callbackData, int eventData1, int eventData2) 	//generam anvelopa semnalului
{  AFISAREANVELOPA=1;   
	switch (event)
	{
		case EVENT_COMMIT:
			
			if(AFISAREANVELOPA==1){	
			//LaunchExecutable("python anvelopa.py");
			
			//astept sa fie generate cele doua fisiere
			//Delay(2);
			
			anvelopa = (double *) malloc(npoints * sizeof(double));
			//incarc informatiile privind rata de esantionare si numarul de valori
			FileToArray("anvelopa.txt", anvelopa, VAL_DOUBLE, npoints, 1, VAL_GROUPS_TOGETHER, VAL_GROUPS_AS_COLUMNS, VAL_ASCII);
			PlotY(panel,mainPanel_RAWGRAPH ,anvelopa,npoints, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_BLUE);

			}
			break;
	}
	
	return 0;
}


int CVICALLBACK On_Prev (int panel, int control, int event,
						 void *callbackData, int eventData1, int eventData2) 
{  
	double *temp;
	double *temp1;
	switch (event)
	{
		case EVENT_COMMIT:
			GetCtrlVal(panel,mainPanel_START,&start);
			GetCtrlVal(panel,mainPanel_STOP,&stop);
			temp=(double*)calloc(npoints/6,sizeof(double));//sunetul are 6 secunde
			temp1=(double*)calloc(npoints/6,sizeof(double));
			if(stop>1)
			{
				--stop;
				--start;
				SetCtrlVal(panel,mainPanel_STOP,stop);
				SetCtrlVal(panel,mainPanel_START,start);
				for(int i=0;i<npoints/6;++i)
				{
					temp[i]=filtered[start*npoints/6+i]; //va calcula in temp valoarea filtrata a secundei corespunzatoare
				}
				if(AFISAREANVELOPA==0){				   //daca nu este apasat butonul pentru anvelopa va afisa semnalul waveData pe secunde
					for(int i=0;i<npoints/6;++i)
					{
						temp1[i]=waveData[start*npoints/6+i];
					
					}
					DeleteGraphPlot (panel,mainPanel_RAWGRAPH , -1, VAL_IMMEDIATE_DRAW);
					PlotY (panel, mainPanel_RAWGRAPH , temp1, npoints/6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_RED);
				}
				else
				{
					for(int i=0;i<npoints/6;++i)	   //daca butonul pentru generare anvelopa a fost apasat atunci se va afisa anvelopa pe secunde
					{
						temp1[i]=anvelopa[start*npoints/6+i];
					}
					DeleteGraphPlot (panel,mainPanel_RAWGRAPH , -1, VAL_IMMEDIATE_DRAW);
					PlotY (panel,mainPanel_RAWGRAPH , temp1, npoints/6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_BLUE);
				}
								
				DeleteGraphPlot (panel, mainPanel_FILTEREDGRAPH, -1, VAL_IMMEDIATE_DRAW);
				PlotY (panel, mainPanel_FILTEREDGRAPH, temp, npoints/6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_MAGENTA);
				
			} 

			break;
	}
	return 0;
}

int CVICALLBACK On_Next (int panel, int control, int event,
						 void *callbackData, int eventData1, int eventData2)		  //utilizata pt a creste secunda pe care se reprezinta semnalul
{
	double *temp;
	double *temp1;
	switch (event)
	{
		case EVENT_COMMIT:
			GetCtrlVal(panel, mainPanel_START  ,&start);
			GetCtrlVal(panel, mainPanel_STOP ,&stop);
			temp=(double*)calloc(npoints/6,sizeof(double));
			temp1=(double*)calloc(npoints/6,sizeof(double));
			if(stop<6)
			{
				
				++stop;
				++start;
				SetCtrlVal(panel, mainPanel_STOP,stop);
				SetCtrlVal(panel, mainPanel_START  ,start);
				for(int i=0;i<npoints/6;++i)
				{
					temp[i]=filtered[start*npoints/6+i];
				}
				if(AFISAREANVELOPA==0){
					for(int i=0;i<npoints/6;++i)
					{
						temp1[i]=waveData[start*npoints/6+i];
					
					}
					DeleteGraphPlot (panel,mainPanel_RAWGRAPH  , -1, VAL_IMMEDIATE_DRAW);
					PlotY (panel, mainPanel_RAWGRAPH  , temp1, npoints/6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_RED);
				}
				else
				{
					for(int i=0;i<npoints/6;++i)
					{
						temp1[i]=anvelopa[start*npoints/6+i];
					}
					DeleteGraphPlot (panel,mainPanel_RAWGRAPH  , -1, VAL_IMMEDIATE_DRAW);
					PlotY (panel, mainPanel_RAWGRAPH  , temp1, npoints/6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_BLUE);
				}
				
								
				DeleteGraphPlot (panel,  mainPanel_FILTEREDGRAPH, -1, VAL_IMMEDIATE_DRAW);
				PlotY (panel,  mainPanel_FILTEREDGRAPH, temp, npoints/6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_MAGENTA);
				
			
			} 


			break;
	}
	return 0;
}


int CVICALLBACK OnSwitchPanel (int panel, int control, int event,
							   void *callbackData, int eventData1, int eventData2)
{
	switch (event)
	{
		case EVENT_COMMIT:
			
			
			if(panel==panelHandle)
			{
				SetCtrlVal(freqPanel, FREQ_PANEL_BINARYSWITCH , 1);
				DisplayPanel(freqPanel);
				HidePanel(panel);
			}
			else
			{
				
					
					SetCtrlVal(panelHandle, mainPanel_BINARYSWITCH_3, 0);
					DisplayPanel(panelHandle);
					HidePanel(freqPanel);
				
				
			}
			break;

	}
	return 0;
}


int CVICALLBACK OnFreqBTT(int panel, int control, int event,void *callbackData, int eventData1, int eventData2)
{	
	int N;
	GetCtrlVal(freqPanel,FREQ_PANEL_RING, &N);
	WindowConst winConst;
	double *convertedSpectrum; //vectorul ce contine spectrul semnalului convertit in volti
	double *autoSpectrum; //spectrul de putere
	double df=0.0; //pasul in domeniul frecventei
	double freqPeak=0.0; //valoarea maxima din spectrul de putere
	double powerPeak=0.0; //frecventa estimata pentru spectrum de putere
  	char unit[32]="V";  //voltage semnal
	int secunda;
	convertedSpectrum=(double*)malloc(N*sizeof(double));
	autoSpectrum=(double*)malloc(N*sizeof(double));


	switch (event)
	{


		case EVENT_COMMIT:
			GetCtrlVal(panel, FREQ_PANEL_Secunda, &secunda);    


			ScaledWindowEx (waveData,N, RECTANGLE_, 0, &winConst);
			//se calculeaza partea pozitiva a spectrului scalat de putere pentru un semnal esantionat
			AutoPowerSpectrum(waveData,N, 1.0/sampleRate, autoSpectrum, &df);
			//calculeaza puterea si frecventa corespunzatoare varfului din spectrul semnalului
			PowerFrequencyEstimate(autoSpectrum,N,-1.0,winConst,df,7,&freqPeak,&powerPeak);

			//afiseaza pe interfata valorile determinate
			SetCtrlVal(freqPanel,FREQ_PANEL_FREQUENCY_PEAK,freqPeak);
			SetCtrlVal(freqPanel,FREQ_PANEL_POWER_PEAK,powerPeak);

			//se converteste spectrul de intrare în formate ce permit o reprezentare grafica mai convenabila
			SpectrumUnitConversion(autoSpectrum, N,0, SCALING_MODE_LINEAR, DISPLAY_UNIT_VRMS, df, winConst, convertedSpectrum, unit);
			//afisam spectrul pe grafic
			
			
			DeleteGraphPlot(panel,FREQ_PANEL_GRAPH_SPECTRUM,-1,VAL_IMMEDIATE_DRAW);
			PlotWaveform(panel,  FREQ_PANEL_GRAPH_SPECTRUM, convertedSpectrum,N/2 ,VAL_DOUBLE, 1.0, 0.0, 0.0, df,VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID,  VAL_CONNECTED_POINTS, VAL_GREEN);

			DeleteGraphPlot(panel,FREQ_PANEL_GRAPH_RAW_DATA,-1,VAL_IMMEDIATE_DRAW);
			PlotY(panel, FREQ_PANEL_GRAPH_RAW_DATA, waveData +  secunda*npoints / 6, npoints / 6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_RED);  

		break;
	}
return 0;
	
}


int CVICALLBACK OnAPPLYBTT(int panel, int control, int event,
							 void *callbackData, int eventData1, int eventData2)
{    	
  	int tipFereastra;
	int secunda;
	int numarFerestre;
	double* window;
	double* windowing;
	double* powerSpectrum;
	double* spectrum;
	int tipFiltru;
	double df;
	static WindowConst winConst;
	int fpass, fstop;
	int cutOff;
	char unit[32] = "V";  //voltage semnal
	double * signal;
	double* filteredSignal = 0;
	double freqPeak=0.0;
	double powerPeak=0.0;
	
	
	switch (event)
	{
		case EVENT_COMMIT:
					GetCtrlVal(panel, FREQ_PANEL_WINDOW_TYPE, &tipFereastra);
					GetCtrlVal(panel, FREQ_PANEL_Secunda, &secunda);
					GetCtrlVal(panel, FREQ_PANEL_RING, &N);
					
		numarFerestre = (int)(((double)npoints / 6) / N);   //puncte per secunda per numarul de puncte ales de pe interfata
		if(numarFerestre <(((double)npoints / 6) / N) )		//nr de ferestre necesare pt o secunda din semnal si pt N puncte folosite
			numarFerestre++;
	
			window = (double*)malloc((int)(N) * sizeof(double));
			windowing = (double*)malloc((int)((6*numarFerestre*N)*sizeof(double))); //secundele semnalului inmultit cu numarul de ferestre necesar pt acel N puncte
			powerSpectrum = (double*)malloc((int)(npoints / 6) * sizeof(double));
			spectrum = (double*)malloc((int)(npoints / 6) * sizeof(double));
			signal = (double*) malloc((6*numarFerestre*N)*sizeof(double)); 
			filteredSignal = (double*) malloc((6*numarFerestre*N)*sizeof(double)); 
			
			for (int i = 0; i < N; i++)
			{
				window[i] = 1.0;
			}
														  //cand impartim nr de esantionare la N va rezulta un numar cu virgula,unde acele nr nu sunt intr-o fereastra completa
			for (int i = 0; i < 6*numarFerestre*N; i++)  //nr care nu sunt intr-o fereastra completa le vom inlocui cu 0 pt a putea aplica fereastra si filtru pe N puncte
			{
					if(i < npoints)
						signal[i ] = waveData[i];
					else
						signal[i] = 0;
	}
	
	//filtrare pe N puncte si pe secunde 
	GetCtrlVal(panel, FREQ_PANEL_FILTER_TYPE, &tipFiltru);
	switch (tipFiltru)
	{
		case 0:
			for(int i=0;i<numarFerestre;i++)
			{
				Bssl_LPF(signal + i * N + secunda*npoints/6,N,sampleRate,(sampleRate/6),4,filteredSignal + i * N + secunda*npoints/6);
			}	
			break;
		case 1:
			for(int i=0;i<numarFerestre;i++)
			{
				Bw_LPF(signal + i * N + secunda*npoints/6,N,sampleRate,(sampleRate/6),6,filteredSignal + i * N+ secunda*npoints/6);
			}	
			break;
	}
	
	for (int i = 0; i <6*numarFerestre*N; i++)
	{
		if(i < npoints)
			signal[i ] = filteredSignal[i];
		else
			signal[i] = 0;
	}
	
	//afisam semnalul filtrat
	DeleteGraphPlot(freqPanel, FREQ_PANEL_GRAPH_2, -1, VAL_IMMEDIATE_DRAW);
	PlotY(panel, FREQ_PANEL_GRAPH_2, filteredSignal + secunda*npoints/6, npoints / 6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_GREEN);
	
	//ferestruire 
	switch (tipFereastra)
	{
		case 0:
			TriWin(window,N);
			break;
		case 1:
			FlatTopWin(window, N);
			break;
	}
	
	for (int i = 0; i < numarFerestre; i++) {
		Mul1D(signal + secunda * npoints / 6 + i * N, window, N, windowing + i * N + secunda*npoints/6);
	}
	//afisam fereastra
	
	DeleteGraphPlot(freqPanel, FREQ_PANEL_GRAPH, -1, VAL_IMMEDIATE_DRAW);
	PlotY(panel, FREQ_PANEL_GRAPH, window, N, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_RED);
	
	//afisam semnalul ferestruit
	
	DeleteGraphPlot(freqPanel, FREQ_PANEL_GRAPH_3, -1, VAL_IMMEDIATE_DRAW);
	PlotY(panel, FREQ_PANEL_GRAPH_3, windowing + secunda*npoints/6, npoints / 6, VAL_DOUBLE, VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_RED);
	
	//calcul spectrum pt semnalul filtrat
	ScaledWindowEx(windowing + secunda* npoints/6,npoints / 6,RECTANGLE,0,&winConst);
	AutoPowerSpectrum(windowing + secunda* npoints/6, npoints / 6, 1.0 / sampleRate, powerSpectrum, &df);
	PowerFrequencyEstimate(powerSpectrum,npoints/12,-1,winConst,df,7,&freqPeak,&powerPeak); 
	SpectrumUnitConversion(powerSpectrum, npoints / 12, 0, SCALING_MODE_LINEAR, DISPLAY_UNIT_VPK, df, winConst, spectrum, unit);
	SetCtrlVal(freqPanel,FREQ_PANEL_FREQUENCY_PEAK,freqPeak);
	SetCtrlVal(freqPanel,FREQ_PANEL_POWER_PEAK,powerPeak);
	DeleteGraphPlot(freqPanel, FREQ_PANEL_GRAPH_4, -1, VAL_IMMEDIATE_DRAW);
	PlotWaveform(freqPanel, FREQ_PANEL_GRAPH_4, spectrum, npoints / 12, VAL_DOUBLE, 1.0, 0.0, 0.0, df,VAL_THIN_LINE, VAL_EMPTY_SQUARE, VAL_SOLID, VAL_CONNECTED_POINTS, VAL_GREEN);

			break;
	}
	return 0;  
 
 
	
}	  

int CVICALLBACK OnSaveBtt (int panel, int control, int event,
						   void *callbackData, int eventData1, int eventData2)			//fct pt salvarea graph-urilor din cel de al doilea panel
{   char fileNameSignal[140];
	int bitmapID;
	int tipFereastra;
	int tipFiltru;
	int secunda;
	
	switch (event)
	{
		case EVENT_COMMIT:
				
				//salvare pe secunde
				//spectru initial
				GetCtrlVal(panel, FREQ_PANEL_Secunda, &secunda);
				GetCtrlVal(panel, FREQ_PANEL_RING, &N);
				sprintf(fileNameSignal, "grafic_spectru_initial_numar_de_puncte_%d.jpg",N);
				GetCtrlDisplayBitmap(panel, FREQ_PANEL_GRAPH_SPECTRUM, 1, &bitmapID);
				SaveBitmapToJPEGFile(bitmapID, fileNameSignal, JPEG_PROGRESSIVE, 100);
				DiscardBitmap(bitmapID);
				
				//semnal ferestruit
				GetCtrlVal(panel, FREQ_PANEL_WINDOW_TYPE, &tipFereastra);
				GetCtrlVal(panel, FREQ_PANEL_RING, &N);
				GetCtrlVal(panel, FREQ_PANEL_FILTER_TYPE, &tipFiltru);
				if(tipFiltru == 0)
					sprintf(fileNameSignal, "grafic_semnal_Filtru_Bessel_ordin_4_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
				else if(tipFiltru ==1 )
					sprintf(fileNameSignal, "grafic_semnal_Filtru_Butterworth_ordin_6_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
				
				GetCtrlDisplayBitmap(panel, FREQ_PANEL_GRAPH_2, 1, &bitmapID);
				SaveBitmapToJPEGFile(bitmapID, fileNameSignal, JPEG_PROGRESSIVE, 100);
				DiscardBitmap(bitmapID);
				
				//semnal filtrat si ferestruit
				GetCtrlVal(panel, FREQ_PANEL_WINDOW_TYPE, &tipFereastra);
				GetCtrlVal(panel, FREQ_PANEL_RING, &N);
				if(tipFereastra == 0)
				{	if(tipFiltru == 0)
						sprintf(fileNameSignal, "grafic_semnal_Fereastra_Triunghiulara_Filtru_Bessel_ordin4_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
	
					else if(tipFiltru==1)
						sprintf(fileNameSignal, "grafic_semnal_Fereastra_Triunghiulara_Filtru_Butterworth_ordin_6_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
					}		
				else if(tipFereastra==1)
				{	
					if(tipFiltru == 0)
						sprintf(fileNameSignal, "grafic_semnal_Fereastra_FlatTop_Filtru_Bessel_ordin4_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
					
					else if(tipFiltru==1)
						sprintf(fileNameSignal, "grafic_semnal_Fereastra_FlatTop_Filtru_Butterworth_ordin_6_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
				}
				GetCtrlDisplayBitmap(panel, FREQ_PANEL_GRAPH_3, 1, &bitmapID);
				SaveBitmapToJPEGFile(bitmapID, fileNameSignal, JPEG_PROGRESSIVE, 100);
				DiscardBitmap(bitmapID);
				
				//spectru semnal ferestruit si filtrat
				GetCtrlVal(panel, FREQ_PANEL_WINDOW_TYPE, &tipFereastra);
				GetCtrlVal(panel, FREQ_PANEL_RING, &N);
				GetCtrlVal(panel, FREQ_PANEL_FILTER_TYPE, &tipFiltru);
				if(tipFereastra == 0)
				{	if(tipFiltru == 0)
						sprintf(fileNameSignal, "grafic_spectru_Fereastra_Triunghiulara_Filtru_Bessel_ordin4_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
					else if(tipFiltru==1)
						sprintf(fileNameSignal, "grafic_spectru_Fereastra_Triunghiulara_Filtru_Butterworth_ordin_6_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
					}		
				else if(tipFereastra==1)
				{	
					if(tipFiltru == 0)
						sprintf(fileNameSignal, "grafic_spectru_Fereastra_FlatTop_Filtru_Bessel_ordin4_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
					else if(tipFiltru==1)
						sprintf(fileNameSignal, "grafic_spectru_Fereastra_FlatTop_Filtru_Butterworth_ordin_6_numar_de_puncte_%d_secunde_%d_%d.jpg",N,secunda,secunda+1);
				}
				GetCtrlDisplayBitmap(panel, FREQ_PANEL_GRAPH_4, 1, &bitmapID);
				SaveBitmapToJPEGFile(bitmapID, fileNameSignal, JPEG_PROGRESSIVE, 100);
				DiscardBitmap(bitmapID);
			break;
	}
	return 0;
}


