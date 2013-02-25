//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.WPF;
using Microsoft.Practices.CompositeUI;

namespace Microsoft.Practices.QuickStarts.WPFIntegration.OrganizationChart
{
    [SmartPart]
    public partial class OrgChartView : UserControl, IOrgChartView, IDisposable
    {
        private OrgChartViewPresenter _presenter;
        private IWPFUIElementAdapter _WPFUIElementAdapter;
        private bool _isDisposing = false;

        public OrgChartView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        [CreateNew]
        public OrgChartViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        [ServiceDependency]
        public IWPFUIElementAdapter WPFUIElementAdapter
        {
            get { return _WPFUIElementAdapter; }
            set { _WPFUIElementAdapter = value; }
        }

        public void OnLoad(object sender, RoutedEventArgs e)
        {
            _presenter.OnViewReady();
        }

        #region IDisposable Members

        ~OrgChartView()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposing) // avoid recursion on Dispose
            {
                if (disposing)
                {
                    _isDisposing = true;
                    if (_presenter != null)
                    {
                        _presenter.Dispose();
                        _presenter = null;
                    }                    
                    if (_WPFUIElementAdapter != null)
                    {
                        System.Windows.Forms.Control host = _WPFUIElementAdapter.Wrap(this);                        
                        if (!host.Disposing)
                        {
                            host.Dispose();
                        }
                        _WPFUIElementAdapter = null;
                    }
                }
            }
        }

        #endregion
        
        public static readonly DependencyProperty OrgChartDataSourceProperty =
            DependencyProperty.Register("OrgChartDataSource", typeof(XmlDocument), typeof(OrgChartView), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnValueChanged)));        

        public XmlDocument OrgChartDataSource
        {
            get { return (XmlDocument)GetValue(OrgChartDataSourceProperty); }
            set { SetValue(OrgChartDataSourceProperty, value); }
        }
        
        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            OrgChartView control = (OrgChartView)obj;
            XmlDataProvider provider = (XmlDataProvider)control.Resources["xdpOrgChart"];
            provider.Document = (XmlDocument)args.NewValue;
        }

        public void SelectionChanged(object sender, RoutedEventArgs e)
        {
            XmlElement element = ((XmlElement)((TreeView)e.Source).SelectedItem);
            _presenter.OnPositionSelected(element);
        }


        #region IOrgChartView Members

        public void ShowOrgChart(XmlDocument chart)
        {
            OrgChartDataSource = chart;
        }

        #endregion

        
    }    
}