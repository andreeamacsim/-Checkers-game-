   M S B A M L   `   `   `  ����C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CheckersCheckers  5   	   9!clr-namespace:Checkers.ViewModelsCheckers.ViewModels  5   	   7 clr-namespace:Checkers.ForDesignCheckers.ForDesign  5	   	       Checkers.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Checkers     5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	   *vm!clr-namespace:Checkers.ViewModels  5   	   +conv clr-namespace:Checkers.ForDesign  5	   	     � Title$  
MainWindow��5   	   $	��750��6   $
��1200��6)    � DataContext 5      (   "Checkers.ViewModels.BoardViewModel  5   
    � 	Resources 5      2  ,Checkers.ForDesign.BoolToBrushValueConverter   BoolToBrushConverter2   ,Checkers.ForDesign.BoolToVisibilityConverter  BoolToVisibilityConverter.   (Checkers.ForDesign.PlayerToTurnConverter  PlayerToTurnConverter��%W   &	        &	      &	 &       5   
   6)     5   
   6)     5   
   6%   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��4* �6   �� 5      $��* �6    � ColumnDefinitions 5"   
   �� 5#      $��* �6   �� 5$      $
��1.5* �6   �� 5%      $��* �6   .+�5(   
   o� $��0��6   .@�5)      m� $��_File��6   .@�5*      m� $��	_New Game��6    m� Command 6.   �� *NewGameCommand+m� 5+      $��_Save��6    6*   �� *SaveGameCommand+m� 5,      $��_Open��6    6*   �� *OpenGameCommand+m� 5-      $��_Allow Multiple Jump��6    m� IsCheckable$
 True��69    m� 	IsChecked 5.      �� *AllowMultipleJump+ �� Mode$ TwoWay=�m� 5/      $��_Statistics��6    60   �� *ShowStatisticsCommand+50      m� 51      $��_Help��6   .@�52      m� -   5        $��_About��52      6+   54      55      �� 57   
   
�������$��1��5:      $��1��5;      $��6q�5<      
���������58      �� *Cells+��5?      �� 5@      .R�5A      A� 	 A� Rows$	 8��6"   
 A� Columns$
 8��6+   5B      ��5D      �� 5E      .R�5F      �� ��6   �� *DataContext.ClickCellCommand+ �� RelativeSource ��  �� AncestorType" ����5G      �� ��5H      �� 5I      .R�5J   "   �� $��65��6)   $��65��5K   -   ��5L   -   �� *	IsBlack+ �� 	Converter# [  ._�5O   &   �� ��5P   *   �� 5Q   .    �� 
TargetType" ��64    �� Triggers 5R   2   �� 5S   6    �� Binding 6B   �� *	Content+ �� Value RedPawn6^   .a�5T   :   ��  �� Property � �� �� Value 5U   >   A� 5V   B   
���  ��$��35��5W   J   $��35��5X   J   $��Center=�5Y   J   $��Center=�5Z   J   5]   ;   5^   7   �� 5`   6    6B   �� *	Content+ 	WhitePawn6^   .a�5a   :   ��  � �� 5b   >   A� 5c   B   
�������$��35��5d   F   $��35��5e   F   $��Center=�5f   F   $��Center=�5g   F   5j   ;   5k   7   �� 5l   6    6B   �� *	Content+ 	WhiteKing6^   .a�5m   :   ��  � �� 5n   >   �� 5o   B   $	��100��6I   $	��100��6U   $��3 0 0 0q�6b   .+�5p   F   A� 
�������$��35��6[   $��35��6f    �� Left$
 12.5��6r   
 �� Top$ 13��6�   � 5q   F   
��� ��� � Points' �    �� -1��� -1 Zb@KL 6Z   � 5r   F   
��� ���' �   �RW -1`4< -1���@KL 6Z   � 5s   F   
��� ���' �    ��  -1��� -1 -1@KL 6Z   � 5t   F   
��� ���1 �   ��� @KL  -1 -1��� -1�@KL 6Z   5u   C   5x   ;   5y   7   �� 5z   6    6B   �� *	Content+ RedKing6^   .a�5{   :   ��  � �� 5|   >   �� 5}   B   $	��100��6I   $	��100��6U   $��3 0 0 0q�6b   .+�5~   F   A� 
���  ��$��35��6]   $��35��6h   $
 12.5��6t   $ 13��6�   � 5   F   
��� ���' �    �� -1��� -1 Zb@KL 6Z   � 5�   F   
��� ���' �   �RW -1`4< -1���@KL 6Z   � 5�   F   
��� ���' �    ��  -1��� -1 -1@KL 6Z   � 5�   F   
��� ���1 �   ��� @KL  -1 -1��� -1�@KL 6Z   5�   C   5�   ;   5�   7   5�   /   5�   '   5�   #   5�      5�      5�      5�      �� 5�   
   $��Make a move��5�      
�������$��2��5�   2   $��35,383,50,32q�6B   $��1��6X   ��6   �� *MovePieceCommand+6X   5�      5�      