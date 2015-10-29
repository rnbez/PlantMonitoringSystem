from django.conf.urls import patterns, include, url
from rest_framework.urlpatterns import format_suffix_patterns
from views import *

urlpatterns = patterns('',
    url(r'^node/$', NodeList.as_view()),
    url(r'^node/(?P<pk>[0-9]+)/$', NodeDetail.as_view()),
    url(r'^sensor/$', SensorList.as_view()),
    url(r'^sensor/(?P<pk>[0-9]+)/$', SensorDetail.as_view()),
    url(r'^reading/$', SensorReadingList.as_view()),
    url(r'^reading/(?P<pk>[0-9]+)/$', SensorReadingDetail.as_view()),
)

urlpatterns = format_suffix_patterns(urlpatterns)
