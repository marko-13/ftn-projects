   M S B A M L   `   `   `  ����F  BHCIProjekat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:HCIProjekat.FormeHCIProjekat.Forme  5   	   $    HCIProjekat.Forme.PodaciPrikaz   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:HCIProjekat.Forme     5   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      -   5          � Title$  Prikaz podataka��5
   	   $	��590��6!   $	��470��6.   $	��470��5   	   $	��590��5   	    � Icon$ ../Images/folder.png��5   	    � 	Resources 5         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 btnDodajZivotinju �� 
TargetType	 �� 
TargetType
 ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  ButtonStyle1  ButtonStyle2  ButtonStyle3  MessageBoxStyle1"  Xceed.Wpf.Toolkit.MessageBox0  *Xceed.Wpf.Toolkit.Primitives.WindowControl��%�  &	        &	 �     &	 �     &	 �     &	     &	 .    &	 Q    &	 t    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7 	 &	 c    &	 �    &	 C    &	 �    7  7  7  7 
 �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ #3F3F46��6;   �� 5   
   $ #555555��67   �� 5   
   $ #3F3F46��6>   �� 5   
   $ #007ACC��6:   �� 5    
   $ 	#FFC4E5F6��6<   �� 5!   
   $ 	#FF2C628B��68   �� 5"   
   $ 	#FFF4F4F4��6=   �� 5#   
   $ 	#FFADB2B5��69   �� 5$   
   $ 	#FF838383��6=   �� 5%   
   " ��6*   .�5&      ��  �@�FocusVisualStyle8   61   �� 5'       � �� �#3F3F466+   �� 5(       � �� �#5555556,   �� 5)       � �� �#F1F1F16+   �� 5*       � ��$	 1.5q�60   �� 5+       � ��$ Center=�6;   �� 5,       � ��$ Center=�69   �� 5-       � ��$ 1q�6(   �� 5.       � �� 5/      �� 50      "	 ��6&   .R�51      �� $
 border��6!   #��z��61   #��z��6]   #��z��6�    . ._�52      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  53       54      P� 55       �@��IsDefaulted . . �56   "   �� $ border��6@    � ��
 �  ��6T   �� 57   "    � �� �#3F3F466?   �� 58   "    � �� �#F1F1F16?   59      P� 5:       �@J�IsMouseOver . . �5;   "   �� $ border��6?   8   � ��6S   �� 5<   "   $ border��6@   8   � ��6T   �� 5=   "    � �� �#F1F1F16?   �� 5>   "    � ��$	 1.5q�6D   5?      P� 5@       � �� . . �5A   "   �� $ border��6?   8   � ��6S   �� 5B   "   $ border��6@   8   � ��6T   5C      P� 5D       � {� .  . �5E   "   �� $ border��6?   8   � ��6S   �� 5F   "   $ border��6@   8   � ��6T   �� 5G   "   $ contentPresenter��6K   8   � ��6i   5H      5J      5L      5M      �� 5Q   
   " ��6%   .�5R      ��  � ��
 ����6@   �� 5S       � ��
 � d �6?   5T      �� 5U   
   " ��6%   .�5V      ��  � ��
 �����6@   �� 5W       � ��
 �  ��6=   5X      �� 5Y   
   " ��6%   .�5Z      ��  � ��
 �����6A   �� 5[       � ��
 � ���6@   5\      �� 5^   
   "  6)   .�5_      ��  � �� �	#FF1E1E1E6+   �� 5`       � �� �	#FF9999996,   �� 5a       �@ CaptionForeground �	#FFF1F1F162   �� 5b       �@ WindowBorderBrush �	#FF68686862   �� 5c       �@ WindowBackground �	#FF2D2D3061   �� 5d       �@ WindowOpacity$	 1.0Q�6.   �� 5e       � �� �	#FFF1F1F16+   �� 5f       �@ YesButtonStyle8   6W   �� 5g       �@ NoButtonStyle8  6W   �� 5h       �@ CancelButtonStyle8  6Z   �� 5i       �@ OkButtonStyle8  6\   5j      5l      .��5m      ]� .+�5n   
   o� $	��Top=�6   ��5o      �� 5p      .R�5q      ]� $��Stretch=�6    5r      5s      .@�5z      m� $��Izmenite��6   .@�5{      m� -   5        $��Izmenite zivotinju��5{      {�67   ��  �� ElementName$ TabelaZivotinja�� �� Path$ SelectedItems.Count� m� Icon 5}      �� 5~      $��../Images/pawprint.ico��6    5�      m� 5�      -   5        $��Izmenite tip��5�      {�61   �� $ TabelaTipova��$ SelectedItems.Count� 5�      �� 5�      $��../Images/category.png��6    5�      m� 5�      -   5        $��Izmenite etiketu��5�      {�65   �� $ TabelaEtiketa��$ SelectedItems.Count� 5�      �� 5�      $��../Images/tag.png��6    5�      5�      m� 5�      $��Obrisite��6   .@�5�      m� -   5         m�Name$ MenuItemObrisiZivotinju��5�   8   $��Obrisite zivotinju��6   {�5�      �� $ TabelaZivotinja��$ SelectedItems.Count� 5�      �� 5�      $��../Images/pawprint.ico��6    5�      m� 5�      -   5        $��Obrisite tip��5�      {�5�      �� $ TabelaTipova��$ SelectedItems.Count� 5�      �� 5�      $��../Images/category.png��6    5�      m� 5�      -   5        $��Obrisite etiketu��5�      {�5�      �� $ TabelaEtiketa��$ SelectedItems.Count� 5�      �� 5�      $��../Images/tag.png��6    5�      5�      m� 5�      -   5        $��Pomoc��5�   .   $��Right=�6=   5�      5�      � 5�   
   $
��Left=�6   $��Stretch=�6&   $��Stretch=�6D    � RowDefinitions 5�      �� 5�      $
��Auto �6    �� 5�      $��* �6    �� 5�      $
��Auto �6    �� 5�      $��* �6    �� 5�      $
��Auto �6    �� 5�      $��* �6    �� 5�      $
��Auto �6    .+�5�      �� $��0��6   $��	105,0,0,0q�6%   $��16(�68   $��Center=�6F   $
��Bold"�6c   Tabela zivotinja6�   �� 5�      $��0��6   $
��Left=�6%   Oznaka selektovane:6U   �� 5�      -	   5         ��Name$ OznakaSelektovaneZTextBox��5�   p   $��	120,0,0,0q�6   $��0��6*   $	��200��67   $	��100��6F   $
��Left=�6U   {�.   � DataContext 5�      �� $ TabelaZivotinja��$ SelectedItem� �� UpdateSourceTrigger$ PropertyChanged=���5�      �� $ _oznaka�$ PropertyChanged=� �� Mode$ TwoWay=�.��6f   5�      &   System.Windows.Controls.DataGrid  5�      -
   5          Name$ TabelaZivotinja��5�      $��1��60      
IsReadOnly . $��0,5,5,0q�6   $��1,2,2,1q�6�   
���OO/�$y�Visible=�5�         AlternatingRowBackground
 �����   AutoGenerateColumns .  $ �� IsSynchronizedWithCurrentItem$
 TrueY�5�      ��5�   =   �� $ Pod.listaZivotinja���5�      �� 5�      )  #System.Windows.Controls.DataGridRow"  6   .�5�      -   5        5�         Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ Oznaka��6)   -   5        OznakaZKolona��    Width5  /System.Windows.Controls.DataGridLengthConverter$  * 5�   P   1  +System.Windows.Controls.DataGridBoundColumn!   Binding! 6Z   �� *	_oznaka+5�        5�      $	 Ime��6)   -   5        
imeZKolona��$  * 5�   J   ! 6T   �� *_ime+5�        5�      $	 Tip��6)   -   5        
tipZKOlona��$  * 5�   J   ! 6T   �� *_tip+5�      5�      �� 5�      $��	105,0,0,0q�6   $��2��6+   $��16(�68   $��Center=�6F   $
��Bold"�6c   Tabela tipova6�   �� 5�      $��2��6   $
��Left=�6%   Oznaka selektovanog:6V   �� 5�      -   5        $  OznakaSelektovanogTTextBox��5�   p   $��	120,0,0,0q�6   $��2��6*   $	��200��67   $	��100��6F   $
��Left=�6U   {�.   5�      �� $ TabelaTipova��$ SelectedItem�$ PropertyChanged=���5�      �� $ _oznaka�$ PropertyChanged=�$ TwoWay=�.��6f   5�        5�      -   5        $ TabelaTipova��5�      $��3��6-    . $��0,5,5,0q�6z   $��1,2,2,1q�6�   
���OO/�$y�Visible=�5�      
 ����� .  $
 TrueY�6m   ��5�   :   �� $ Pod.listaTipova���5�      �� 5�      "  6   .�5�      -   5        5�       5�        5�      $ Oznaka��6)   -   5        oznakaTKolona��$  * 5�   P   ! 6Z   �� *	_oznaka+5�        5�      $	 Ime��6)   -   5        
imeTKolona��$  * 5�   J   ! 6T   �� *_ime+5�      5�      �� 5�      $��4��6   $��	105,0,0,0q�6%   $��16(�68   $��Center=�6F   $
��Bold"�6c   Tabela etiketa6�   �� 5�      $��4��6   $
��Left=�6%   Oznaka selektovane:6U   �� 5�      -   5        $ OznakaSelektovaneETextBox��5�   p   $��	120,0,0,0q�6   $��4��6*   $	��200��67   $	��100��6F   $
��Left=�6U   {�.   5�      �� $ TabelaEtiketa��$ SelectedItem�$ PropertyChanged=���5�      �� $ _oznaka�$ PropertyChanged=�$ TwoWay=�.��6f   5�        5�      -   5        $ TabelaEtiketa��5�      $��5��6.    . $��0,5,5,0q�6|   $��1,2,2,1q�6�   
���OO/�$y�Visible=�5�      
 ����� .  $
 TrueY�6m   ��5�   ;   �� $ Pod.listaEtiketa���5�      �� 5�      "  6   .�5�      -   5        5�       5�        5�      $ Oznaka��6)   -   5        oznakaEKolona��$  * 5�   P   ! 6Z   �� *	_oznaka+5�      5�      �� 5�      -   5        $��6��5�      $��Right=�6"   $��0,5,25,5q�6>   #��� 
 5  2   .��5     
Povratak5     5     5     5	     