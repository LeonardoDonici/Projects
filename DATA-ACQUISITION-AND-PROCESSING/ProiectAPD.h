/**************************************************************************/
/* LabWindows/CVI User Interface Resource (UIR) Include File              */
/*                                                                        */
/* WARNING: Do not add to, delete from, or otherwise modify the contents  */
/*          of this include file.                                         */
/**************************************************************************/

#include <userint.h>

#ifdef __cplusplus
    extern "C" {
#endif

     /* Panels and Controls: */

#define  FREQ_PANEL                       1       /* callback function: OnFreqPanel */
#define  FREQ_PANEL_BINARYSWITCH          2       /* control type: binary, callback function: OnSwitchPanel */
#define  FREQ_PANEL_RING                  3       /* control type: ring, callback function: (none) */
#define  FREQ_PANEL_GRAPH_SPECTRUM        4       /* control type: graph, callback function: (none) */
#define  FREQ_PANEL_GRAPH_RAW_DATA        5       /* control type: graph, callback function: (none) */
#define  FREQ_PANEL_COMMANDBUTTON         6       /* control type: command, callback function: OnFreqBTT */
#define  FREQ_PANEL_Secunda               7       /* control type: ring, callback function: (none) */
#define  FREQ_PANEL_FREQUENCY_PEAK        8       /* control type: numeric, callback function: (none) */
#define  FREQ_PANEL_POWER_PEAK            9       /* control type: numeric, callback function: (none) */
#define  FREQ_PANEL_GRAPH_4               10      /* control type: graph, callback function: (none) */
#define  FREQ_PANEL_GRAPH_3               11      /* control type: graph, callback function: (none) */
#define  FREQ_PANEL_GRAPH_2               12      /* control type: graph, callback function: (none) */
#define  FREQ_PANEL_GRAPH                 13      /* control type: graph, callback function: (none) */
#define  FREQ_PANEL_FILTER_TYPE           14      /* control type: ring, callback function: (none) */
#define  FREQ_PANEL_WINDOW_TYPE           15      /* control type: ring, callback function: (none) */
#define  FREQ_PANEL_COMMANDBUTTON_2       16      /* control type: command, callback function: OnAPPLYBTT */
#define  FREQ_PANEL_COMMANDBUTTON_3       17      /* control type: command, callback function: OnSaveBtt */

#define  mainPanel                        2       /* callback function: OnMainPanel */
#define  mainPanel_RAWGRAPH               2       /* control type: graph, callback function: (none) */
#define  mainPanel_FILTEREDGRAPH          3       /* control type: graph, callback function: (none) */
#define  mainPanel_COMMANDBUTTON          4       /* control type: command, callback function: On_Prev */
#define  mainPanel_COMMANDBUTTON_2        5       /* control type: command, callback function: On_Next */
#define  mainPanel_TEXTMSG                6       /* control type: textMsg, callback function: (none) */
#define  mainPanel_TEXTMSG_2              7       /* control type: textMsg, callback function: (none) */
#define  mainPanel_COMMANDBUTTON_3        8       /* control type: command, callback function: OnAplica */
#define  mainPanel_NUMERIC                9       /* control type: numeric, callback function: (none) */
#define  mainPanel_COMMANDBUTTON_4        10      /* control type: command, callback function: OnLoadCB */
#define  mainPanel_COMMANDBUTTON_5        11      /* control type: command, callback function: OnMaxMin */
#define  mainPanel_NUMERIC_2              12      /* control type: numeric, callback function: (none) */
#define  mainPanel_NUMERIC_3              13      /* control type: numeric, callback function: (none) */
#define  mainPanel_NUMERIC_4              14      /* control type: numeric, callback function: (none) */
#define  mainPanel_NUMERIC_5              15      /* control type: numeric, callback function: (none) */
#define  mainPanel_NUMERIC_7              16      /* control type: numeric, callback function: (none) */
#define  mainPanel_NUMERIC_6              17      /* control type: numeric, callback function: (none) */
#define  mainPanel_NUMERIC_8              18      /* control type: numeric, callback function: (none) */
#define  mainPanel_BINARYSWITCH           19      /* control type: binary, callback function: OnFiltruCB */
#define  mainPanel_NUMERIC_9              20      /* control type: numeric, callback function: (none) */
#define  mainPanel_BINARYSWITCH_2         21      /* control type: binary, callback function: (none) */
#define  mainPanel_STOP                   22      /* control type: numeric, callback function: (none) */
#define  mainPanel_START                  23      /* control type: numeric, callback function: (none) */
#define  mainPanel_HiISTOGRAM_GRAPH       24      /* control type: graph, callback function: (none) */
#define  mainPanel_COMMANDBUTTON_6        25      /* control type: command, callback function: OnSaveButton */
#define  mainPanel_COMMANDBUTTON_7        26      /* control type: command, callback function: Generare_Anvelopa */
#define  mainPanel_NUMERIC_11             27      /* control type: numeric, callback function: (none) */
#define  mainPanel_NUMERIC_10             28      /* control type: numeric, callback function: (none) */
#define  mainPanel_BINARYSWITCH_3         29      /* control type: binary, callback function: OnSwitchPanel */


     /* Control Arrays: */

          /* (no control arrays in the resource file) */


     /* Menu Bars, Menus, and Menu Items: */

          /* (no menu bars in the resource file) */


     /* Callback Prototypes: */

int  CVICALLBACK Generare_Anvelopa(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK On_Next(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK On_Prev(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnAplica(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnAPPLYBTT(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnFiltruCB(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnFreqBTT(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnFreqPanel(int panel, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnLoadCB(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnMainPanel(int panel, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnMaxMin(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnSaveBtt(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnSaveButton(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK OnSwitchPanel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);


#ifdef __cplusplus
    }
#endif