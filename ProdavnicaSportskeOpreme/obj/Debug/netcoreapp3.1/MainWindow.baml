   M S B A M L   `   `   `  ����S  OProdavnicaSportskeOpreme, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ProdavnicaSportskeOpremeProdavnicaSportskeOpreme  5   	   )    #ProdavnicaSportskeOpreme.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:ProdavnicaSportskeOpreme     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   � -   5         �Name$ frmMain��5
      
�������.+�5      &   System.Windows.Controls.DataGrid  -   5        $��dataGridCentralni��5      $��Center=�60   $��Center=�6M   $
��auto��6h   $
��auto��6v      
IsReadOnly$
 True��6�      AlternatingRowBackground
 ���@�   CanUserAddRows$ False��5   6      SelectionUnit$ FullRow=�6M      SelectionMode$ Single=�6e   $��DemiBold"�6|   �� 5      $��Center=�6   $��30��66   $	��Top=�6C   $
��auto��6[    �� Orientation$ 
Horizontal=�6h   .+�5      �� -   5         ��Name$ btnKupci��5      $��Kupci��6+   $
��Left=�6;   $��30��6V   $	��Top=�6b   $��75��6z   $��DemiBold"�6�   
���   �
�����@�6�   �� 5      -   5        $ btnProdavci��5      $��Prodavci��6.   $
��Left=�6A   $��30��6\   $	��Top=�6h   $��75��6�   $��DemiBold"�6�   
���   �
�����@�6�   �� 5      -   5        $ btnProizvodi��5      $��	Proizvodi��6/   $
��Left=�6C   $��30��6^   $	��Top=�6j   $��75��6�   $��DemiBold"�6�   
���   �
�����@�6�   �� 5      -   5        $ 
btnDostave��5      $��Dostave��6-   $
��Left=�6?   $��30��6Z   $	��Top=�6f   $��75��6~   $��DemiBold"�6�   
���   �
�����@�6�   �� 5      -   5        $ btnGarancije��5      $��	Garancije��6/   $
��Left=�6C   $��30��6^   $	��Top=�6j   $��75��6�   $��DemiBold"�6�   
���   �
�����@�6�   �� 5      -   5        $ btnPlacanja��5      $��Placanja��6.   $��30��6A   $	��Top=�6M   $��98��6e   $��DemiBold"�6�   
���   �
�����@�6�   �� 5      -	   5        $ btnTipPlacanja��5      $��Tip Placanja��61   $��30��6H   $	��Top=�6T   $��98��6l   $��DemiBold"�6�   
���   �
�����@�6�   �� 5      -
   5        $ btnTransakcije��5      $��Transakcije��61   $��30��6G   $	��Top=�6S   $��98��6k   $��DemiBold"�6�   
���   �
�����@�6�   5      �� 5      $��Right=�6   $��30��65   $��Bottom=�6B   $	��360��6]   $ 
Horizontal=�6i   .+�5      �� -   5        $ btnDodaj��5      $��Dodaj��6+   $	��120��6;   $��DemiBold"�6^   
���   �
�����@�6�   �� 5      -   5        $ 	btnObrisi��5      $��Obriši��6,   $	��120��6=   $��DemiBold"�6I   
���   �
�����@�6�   �� 5      -   5        $ 	btnIzmeni��5      $��Izmeni��6,   $	��120��6=   $��DemiBold"�6a   
���   �
�����@�6�   5      5      5      5      