#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include <stdio.h>
#include <stdlib.h>
 

void Build(){
	HKEY key;
	if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", 0, KEY_ALL_ACCESS, &key) == ERROR_SUCCESS)
	{
		CHAR szBuf[MAX_PATH];
	  	DWORD dwBufLen = MAX_PATH;
	  	
	    RegQueryValueEx(key, "CurrentBuild", NULL, NULL,(BYTE*) szBuf,&dwBufLen);
		
	    RegCloseKey(key);
	    
	    printf ("%s\n", szBuf);
	    int build = atoi(szBuf);
	    if (build >= 7600)
		{
    		 printf ("Windows Seven");	
			 return; 
		}
		if (build >= 6000){  
			printf ("Windows Vista"); 
			return;
	    } 
	    if (build >= 3790)
		{
			 printf ("Windows Server 2003"); 
			 return;
		}
    	if (build >= 2600)
			{
	    	printf ("Windows XP");
	    	return;
	    }  	    		 	
		if (build >= 2195)  {
	 		printf ("Windows 2000");
	 		return;
	 	}
	    printf("\n");
	}
}

void Version(){
	HKEY key;
	if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", 0, KEY_ALL_ACCESS, &key) == ERROR_SUCCESS)
	{
		CHAR szBuf[MAX_PATH];
	  	DWORD dwBufLen = MAX_PATH;
	  	
	    RegQueryValueEx(key, "ProductName", NULL, NULL,(BYTE*) szBuf,&dwBufLen);
		
	    RegCloseKey(key);
	    
	    printf ("%s\n", szBuf);
	    
	    
	}
}

    
int main(){
//	Version();
	Build();
//	printf("%s", Version());
	char* s;
	scanf("%s", s);
	return 0;
}